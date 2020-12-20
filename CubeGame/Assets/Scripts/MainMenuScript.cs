using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Options;

    private void Start()
    {
        CoinRotation.coinList.Clear();
        CoinRotation.totalCoins = 0;
        CoinRotation.currentScore = 0;
        CharacterMechanics.seconds = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
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
