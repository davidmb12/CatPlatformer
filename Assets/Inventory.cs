using NarvalDev.Core;
using NarvalDev.Gameplay;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NarvalDev.Runner
{
    public class Inventory : AbstractSingleton<Inventory>
    {
        [SerializeField]
        GenericGameEventListener m_CoinListener;
        [SerializeField]
        GenericGameEventListener m_BlueMilkListener;
        [SerializeField]
        GenericGameEventListener m_WinEventListener;
        [SerializeField]
        GenericGameEventListener m_LoseEventListener;

        int m_TempCoins;
        int m_TotalCoins;

        [SerializeField] Hud m_Hud;

        LevelCompleteScreen m_LevelCompleteScreen;

        private void Start()
        {
            m_CoinListener.EventHandler = OnCoinPicked;
            m_BlueMilkListener.EventHandler = OnBlueMilkListener;

            m_TempCoins = 0;

        }
        private void OnEnable()
        {
            m_CoinListener.Subscribe();
            m_BlueMilkListener.Subscribe();
        }

        private void OnDisable()
        {
            m_CoinListener.Unsubscribe();
            m_BlueMilkListener.Unsubscribe();
        }
        void OnCoinPicked()
        {
            if(m_CoinListener.m_Event is ItemPickedEvent coinPickedEvent)
            {
                m_TempCoins += coinPickedEvent.Count;
                m_Hud.PointsValue = m_TempCoins;
            }
        }

        void OnBlueMilkListener()
        {
            if (m_CoinListener.m_Event is ItemPickedEvent blueMilkPickedEvent)
            {
                PlayerHealth.Instance.ResetGrayscaleTime();
            }
        }


    }

}
