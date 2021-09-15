using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsBehaviour : MonoBehaviour
{
    public static SettingsBehaviour SettingsInstance;
    //Options Variables
    //------------------------------------------------------------------------------------
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    Resolution[] _resolutions;
    public Dropdown ResolutionDropdown;
    //------------------------------------------------------------------------------------

    #region Awake
    void Awake()
    {
        SettingsInstance = this;
    }
    #endregion


    #region Start
    void Start()
    {
        _resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    #endregion
    

    #region OptionsSettings
    public void SetMusicVolume(float MusicVolume)
    {
        MusicMixer.SetFloat("MusicVolume", MusicVolume);
    }

    public void SetSFXVolume(float SFXVolume)
    {
        SFXMixer.SetFloat("SFXVolume", SFXVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    #endregion
}
