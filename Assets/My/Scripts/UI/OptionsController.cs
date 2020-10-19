using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OptionsController : MonoBehaviour
{
    [Header("Main Panel")]
    [SerializeField] private Transform _optionsPanel;
    [Header("Panels")]
    [SerializeField] private Transform _mainPanel;
    [SerializeField] private Transform _settingsPanel;
    [Header("Settings references")]
    [SerializeField] private Toggle _shadowsToggle;
    [SerializeField] private Toggle _postProcessingToggle;
    [SerializeField] private Toggle _audioMuteToggle;
    [SerializeField] private Slider _audioVolumeSlider;
    [SerializeField] private TMP_Dropdown _qualityLevelDropdown;
    [Header("Other references")]
    [SerializeField] private PostProcessVolume _postProcessing;

    private bool _isPostProcessingEnabled = true;

    private void Start()
    {
        if (QualitySettings.shadows == ShadowQuality.Disable)
            _shadowsToggle.isOn = false;
        else
            _shadowsToggle.isOn = true;

        if (AudioListener.volume <= 0)
            _audioMuteToggle.isOn = true;
        else
            _audioMuteToggle.isOn = false;

        _audioVolumeSlider.value = AudioListener.volume;


        //switch like this is becouse there are more quality levels then dropdown options so its configured like this.
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                _qualityLevelDropdown.value = 0;
                break;
            case 1:
                _qualityLevelDropdown.value = 1;
                break;
            case 2:
                _qualityLevelDropdown.value = 2;
                break;
            case 3:
                _qualityLevelDropdown.value = 3;
                break;
        }

        if (_postProcessingToggle != null)
            _postProcessingToggle.isOn = _isPostProcessingEnabled;

        if (_postProcessing != null)
        {
            _postProcessing.gameObject.SetActive(_isPostProcessingEnabled);
        }

        _mainPanel.gameObject.SetActive(true);
        _settingsPanel.gameObject.SetActive(false);
        _optionsPanel.gameObject.SetActive(false);

    }

    public void OpenMenu()
    {
    }

    public void ChangeShadowSettings(bool p_toggleValue)
    {
        if (p_toggleValue)
        {
            QualitySettings.shadows = ShadowQuality.HardOnly;
        }
        else
        {
            QualitySettings.shadows = ShadowQuality.Disable;
        }
    }

    public void ChangeAudioMute(bool p_toggleValue)
    {
        if (p_toggleValue)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = _audioVolumeSlider.value;
        }
    }

    public void ChangeAudioVolume()
    {
        if (_audioMuteToggle.isOn)
            return;

        AudioListener.volume = _audioVolumeSlider.value;
    }

    public void ChangePostProcessing(bool p_value)
    {
        if (_postProcessing != null)
            _postProcessing.gameObject.SetActive(p_value);
    }

    public void ChangeQualitySettings(int p_value)
    {
        QualitySettings.SetQualityLevel(p_value,true);

        //Set toggle button to match quality settings
        if (QualitySettings.shadows == ShadowQuality.Disable)
            _shadowsToggle.isOn = false;
        else
            _shadowsToggle.isOn = true;

    }

}
public enum QualitySettingsLevel {LowQuality,MediumQuality,HighQuality,UltraQuality }