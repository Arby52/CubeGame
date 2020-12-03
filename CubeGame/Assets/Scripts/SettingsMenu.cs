using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;

        List<string> resList = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRate;
            resList.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resList);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        graphicsDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetMasterVolume(float _volume)
    {
        mixer.SetFloat("masterVolume", _volume);
    }

    public void SetMusicVolume(float _volume)
    {
        mixer.SetFloat("musicVolume", _volume);
    }

    public void SetSFXVolume(float _volume)
    {
        mixer.SetFloat("sfxVolume", _volume);
    }

    public void SetGraphicsQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void SetFullscreenMode(int _fullscreen)
    {
        switch (_fullscreen)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }        
    }

    public void SetResolution(int _resolution)
    {
        Resolution res = resolutions[_resolution];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }

}
