using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Options;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OptionsOpen()
    {
        Options.SetActive(true);
        MainMenu.SetActive(false);

    }

    public void OptionsClose()
    {
        Options.SetActive(false);
        MainMenu.SetActive(true);
    }
}
