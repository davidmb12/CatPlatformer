using NarvalDev.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace NarvalDev.Runner
{
    public class SettingsMenu : View
    {
        [SerializeField]
        HyperCasualButton m_Button;
        [SerializeField]
        Toggle m_EnableMusicToggle;
        [SerializeField]
        Toggle m_EnableSfxToggle;
        [SerializeField]
        Slider m_AudioVolumeSlider;
        [SerializeField]
        Slider m_QualitySlider;

        void OnEnable()
        {
            m_EnableMusicToggle.isOn = AudioManager.Instance.EnableMusic;
            m_EnableSfxToggle.isOn = AudioManager.Instance.EnableSfx;
            m_AudioVolumeSlider.value = AudioManager.Instance.MasterVolume;
            m_QualitySlider.value = QualityManager.Instance.QualityLevel;

            m_Button.AddListener(OnBackButtonClick);
            m_EnableMusicToggle.onValueChanged.AddListener(MusicToggleChanged);
            m_EnableSfxToggle.onValueChanged.AddListener(SfxToggleChanged);
            m_AudioVolumeSlider.onValueChanged.AddListener(VolumneSliderChanged);
            m_QualitySlider.onValueChanged.AddListener(QualitySliderChanged);
        }

        private void QualitySliderChanged(float value)
        {
            QualityManager.Instance.QualityLevel = (int)value;

        }

        private void VolumneSliderChanged(float value)
        {
            AudioManager.Instance.MasterVolume = value;

        }

        private void SfxToggleChanged(bool value)
        {
            AudioManager.Instance.EnableSfx = value;
        }

        private void MusicToggleChanged(bool value)
        {
            AudioManager.Instance.EnableMusic = value;
        }

        private void OnBackButtonClick()
        {
            UIManager.Instance.GoBack();
        }
    }

}
