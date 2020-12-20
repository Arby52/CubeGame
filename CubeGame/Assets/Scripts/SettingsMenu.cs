using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public enum settingsState
    {
        graphics,
        camera,
        controls
    }

    public AudioMixer mixer;

    public GameObject graphicsDisplay;
    public GameObject cameraDisplay;
    public GameObject controlsDisplay;

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public Slider xAxisSens;
    public Slider yAxisSens;
    public Toggle invertY;
    public TMP_Text yText;
    public TMP_Text xText;

    public settingsState currentState;

    public static float masterVol = 1;
    public static float sfxVol = 1;
    public static float musicVol = 1;


    private void Start()
    {
        currentState = settingsState.graphics;
        if (!graphicsDisplay.activeInHierarchy)
        {
            graphicsDisplay.SetActive(true);
            cameraDisplay.SetActive(false);
            controlsDisplay.SetActive(false);
        }

        xAxisSens.value = CameraController.xSensitivity;
        yAxisSens.value = CameraController.ySensitivity;
        invertY.isOn = CameraController.invertYAxis;

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

        mixer.GetFloat("masterVolume", out masterVol);
        masterSlider.value = Mathf.Pow(10, masterVol / 20); 

        mixer.GetFloat("musicVolume", out musicVol);
        musicSlider.value = Mathf.Pow(10, musicVol / 20);

        mixer.GetFloat("sfxVolume", out sfxVol);
        sfxSlider.value = Mathf.Pow(10, sfxVol / 20);
    }

    public void update()
    {
        if (resolutionDropdown.value != QualitySettings.GetQualityLevel())
        {
            resolutionDropdown.value = QualitySettings.GetQualityLevel();
        }
    }

    public void SwitchState(int _state)
    {
        settingsState inputState = (settingsState)_state;

        switch (inputState)
        {
            case settingsState.camera:
                graphicsDisplay.SetActive(false);
                cameraDisplay.SetActive(true);
                controlsDisplay.SetActive(false);
                break;

            case settingsState.graphics:
                graphicsDisplay.SetActive(true);
                cameraDisplay.SetActive(false);
                controlsDisplay.SetActive(false);
                break;

            case settingsState.controls:
                graphicsDisplay.SetActive(false);
                cameraDisplay.SetActive(false);
                controlsDisplay.SetActive(true);
                break;
        }

    }

    public void SetMasterVolume(float _volume)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(_volume) * 20);
    }

    public void SetMusicVolume(float _volume)
    {
        mixer.SetFloat("musicVolume", Mathf.Log10(_volume) * 20);
    }

    public void SetSFXVolume(float _volume)
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(_volume) * 20);
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

    public void SetXSensitivity()
    {
        CameraController.xSensitivity = xAxisSens.value;
        xText.text = "X-Axis Sensitivity: " + xAxisSens.value;
    }
    public void SetYSensitivity()
    {
        CameraController.ySensitivity = yAxisSens.value;
        yText.text = "Y-Axis Sensitivity: " + yAxisSens.value;
    }
    public void SetYInversion(bool _bool)
    {
        CameraController.invertYAxis = _bool;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
