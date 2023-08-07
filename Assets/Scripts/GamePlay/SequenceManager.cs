using NarvalDev.Core;
using NarvalDev.Runner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Sequences;

namespace NarvalDev.Gameplay
{
    public class SequenceManager : AbstractSingleton<SequenceManager>
    {
        [SerializeField]
        GameObject[] m_PreloadedAssets;
        [SerializeField]
        AbstractLevelData[] m_Levels;
        [SerializeField]
        GameObject[] m_LevelManagers;
        public AbstractLevelData[] Levels => m_Levels;
        [Header("Events")]
        [SerializeField]
        AbstractGameEvent m_ContinueEvent;
        [SerializeField]
        AbstractGameEvent m_BackEvent;
        [SerializeField]
        AbstractGameEvent m_WinEvent;
        [SerializeField]
        AbstractGameEvent m_LoseEvent;
        [SerializeField]
        AbstractGameEvent m_PauseEvent;
        [Header("Other")]
        [SerializeField]
        float m_SplashDelay = 2f;
        float m_TransitionDelay = 3f;

        readonly StateMachine m_StateMachine = new();
        IState m_SplashScreenState;
        IState m_MainMenuState;
        IState m_LevelSelectState;
        IState m_InitTransitionState;
        IState m_EndTransitionState;
        IState m_GameStartedState;
        IState m_PausedState;


        readonly List<IState> m_LevelStates = new();
        public IState m_CurrentLevel;

        SceneController m_SceneController;

        /// <summary>
        /// Initializes the SequenceManager
        /// </summary>
        public void Initialize()
        {
            m_SceneController = new SceneController(SceneManager.GetActiveScene());

            InstantiatePreloadedAssets();

            m_SplashScreenState = new State(ShowUI<SplashScreen>);
            m_StateMachine.Run(m_SplashScreenState);

            CreateMenuNavigationSequence();
            AddLevelStates();
            //AddLevelStates();
            //CreateLevelSequences
            //SetStartingLevel(0);
        }

        void InstantiatePreloadedAssets()
        {
            foreach (var asset in m_PreloadedAssets)
            {
                Instantiate(asset);
            }
        }

        void CreateMenuNavigationSequence()
        {
            //Create states
            var splashDelay = new DelayState(m_SplashDelay);
            var transitionDelay = new DelayState(m_TransitionDelay);

            m_MainMenuState = new State(OnMainMenuDisplayed);
            m_LevelSelectState = new State(OnLevelSelectionDisplayed);
            m_InitTransitionState = new State(OnTransitionDisplayed);
            m_EndTransitionState = new State(OnEndTransition);
            m_GameStartedState = new State(OnGameStarted);
            m_PausedState = new State(OnPause);

            var gameLevelState = CreateLevelState("GameScene_RunnerMapGeneration");

            //Connect the states
            m_SplashScreenState.AddLink(new Link(splashDelay));
            splashDelay.AddLink(new Link(m_MainMenuState));
            m_MainMenuState.AddLink(new EventLink(m_ContinueEvent, m_InitTransitionState));
            m_InitTransitionState.AddLink(new EventLink(m_ContinueEvent, transitionDelay));
            transitionDelay.AddLink(new Link(m_EndTransitionState));
            m_EndTransitionState.AddLink(new EventLink(m_ContinueEvent, gameLevelState));
            gameLevelState.AddLink(new EventLink(m_ContinueEvent, m_GameStartedState));
            //m_GameStartedState.AddLink(new EventLink(m_PauseEvent,m_PausedState));

        }

        private void OnGameStarted()
        {
            GameManager.Instance.StartGame();
        }

        // void CreateLevelSequences()
        // {
        //     m_LevelStates.Clear();

        //     //Create and connect all level states
        //     IState lastState = null;
        //     foreach (var level in m_Levels)
        //     {
        //         IState state = null;
        //         if (level is SceneRef sceneLevel)
        //         {
        //             state = CreateLevelState(sceneLevel.m_ScenePath);
        //         }
        //         else
        //         {
        //             state = CreateLevelState(level);
        //         }
        //         lastState = AddLevelPeripheralStates(state, m_LevelSelectState, lastState);
        //     }

        //     //Closing the loop: connect the last level to the level-selection state
        //     var unloadLastScene = new UnloadLastSceneState(m_SceneController);
        //     lastState?.AddLink(new EventLink(m_ContinueEvent, unloadLastScene));
        //     unloadLastScene.AddLink(new Link(m_LevelSelectState));
        // }

        /// <summary>
        /// Creates a level state from a scene
        /// </summary>
        /// <param name="scenePath"></param>
        /// <returns></returns>
        IState CreateLevelState(string scenePath)
        {
            return new LoadSceneState(m_SceneController, scenePath);
        }

        /// <summary>
        /// Creates a level state from a level data
        /// </summary>
        /// <param name="levelData"></param>
        /// <returns></returns>
        IState CreateLevelState(AbstractLevelData levelData)
        {
            return new LoadLevelFromDef(m_SceneController, levelData, m_LevelManagers);
        }

        void AddLevelStates(){
            //var winState = new Core.PauseState(() => OnWinScreenDisplayed(loadLevelState));
            //var loseState = new Core.PauseState(ShowUI<GameoverScreen>);
            var pauseState = new Core.PauseState(ShowUI<PauseScreen>);
            var unloadLose = new UnloadLastSceneState(m_SceneController);
            var unloadPause = new UnloadLastSceneState(m_SceneController);
            
            //m_GameStartedState.AddLink(new EventLink(m_WinEvent, winState));
            //m_GameStartedState.AddLink(new EventLink(m_LoseEvent, loseState));
            m_GameStartedState.AddLink(new EventLink(m_PauseEvent, pauseState));

            //loseState.AddLink(new EventLink(m_ContinueEvent, loadLevelState));
            //loseState.AddLink(new EventLink(m_BackEvent, unloadLose));
            //unloadLose.AddLink(new Link(quitState));

            pauseState.AddLink(new EventLink(m_ContinueEvent, m_GameStartedState));
            pauseState.AddLink(new EventLink(m_BackEvent, unloadPause));
            unloadPause.AddLink(new Link(m_MainMenuState));

            //return winState;

        }
        // IState AddLevelPeripheralStates(IState loadLevelState, IState quitState, IState lastState)
        // {
        //     //Create states
        //     //m_LevelStates.Add(loadLevelState);
        //     //var m_GameStartedState = new State(() => OnGamePlayStarted(loadLevelState));
        //     var winState = new Core.PauseState(() => OnWinScreenDisplayed(loadLevelState));
        //     //var loseState = new Core.PauseState(ShowUI<GameoverScreen>);
        //     var pauseState = new Core.PauseState(ShowUI<PauseMenu>);
        //     var unloadLose = new UnloadLastSceneState(m_SceneController);
        //     var unloadPause = new UnloadLastSceneState(m_SceneController);

        //     //Connect the states
        //     lastState?.AddLink(new EventLink(m_ContinueEvent, loadLevelState));
        //     loadLevelState.AddLink(new Link(gameplayState));

        //     m_GameStartedState.AddLink(new EventLink(m_WinEvent, winState));
        //     m_GameStartedState.AddLink(new EventLink(m_LoseEvent, loseState));
        //     m_GameStartedState.AddLink(new EventLink(m_PauseEvent, pauseState));

        //     loseState.AddLink(new EventLink(m_ContinueEvent, loadLevelState));
        //     loseState.AddLink(new EventLink(m_BackEvent, unloadLose));
        //     unloadLose.AddLink(new Link(quitState));

        //     pauseState.AddLink(new EventLink(m_ContinueEvent, gameplayState));
        //     pauseState.AddLink(new EventLink(m_BackEvent, unloadPause));
        //     unloadPause.AddLink(new Link(m_MainMenuState));

        //     return winState;
        // }

        /// <summary>
        /// Changes the starting gameplay level in the sequence of levels by making a slight change to its links
        /// </summary>
        /// <param name="index">Index of the level to set as starting level</param>
        public void SetStartingLevel(int index)
        {
            m_LevelSelectState.RemoveAllLinks();
            m_LevelSelectState.AddLink(new EventLink(m_ContinueEvent, m_LevelStates[index]));
            m_LevelSelectState.AddLink(new EventLink(m_BackEvent, m_MainMenuState));
            m_LevelSelectState.EnableLinks();
        }

        void ShowUI<T>() where T : View
        {
            UIManager.Instance.Show<T>();
        }

        void OnPause(){
            Debug.Log("Paused");
            ShowUI<PauseScreen>();

        }
        void OnMainMenuDisplayed()
        {
            ShowUI<MainMenu>();
            AudioManager.Instance.PlayMusic(SoundID.MenuMusic);
        }

        void OnTransitionDisplayed()
        {
            ShowUI<TransitionScreen>();

            m_ContinueEvent.Raise();
        }

        void OnEndTransition()
        {
            m_ContinueEvent.Raise();
            UIManager.Instance.GetCurrentView().PlayAnimation("TransitionEndAnim");
        }

        void OnWinScreenDisplayed(IState currentLevel)
        {
            UIManager.Instance.Show<LevelCompleteScreen>();
            var currentLevelIndex = m_LevelStates.IndexOf(currentLevel);

            if (currentLevelIndex == -1)
                throw new Exception($"{nameof(currentLevel)} is invalid!");

            var levelProgress = SaveManager.Instance.LevelProgress;
            if (currentLevelIndex == levelProgress && currentLevelIndex < m_LevelStates.Count - 1)
                SaveManager.Instance.LevelProgress = levelProgress + 1;
        }

        void OnLevelSelectionDisplayed()
        {
            ShowUI<LevelSelectionScreen>();
            AudioManager.Instance.PlayMusic(SoundID.MenuMusic);
        }

        void OnGamePlayStarted(IState current)
        {
            ShowUI<Hud>();
            AudioManager.Instance.StopMusic();
        }
    }

}
