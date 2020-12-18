using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScreenScript : MonoBehaviour
{
    public TMP_Text points;
    public TMP_Text time;

    public void Update()
    {
        points.text = "You scored  " + CoinRotation.currentScore + " out of " + CoinRotation.totalCoins + " points!";
        time.text = "Your time was " + CharacterMechanics.minutes.ToString() + "-" + Mathf.RoundToInt(CharacterMechanics.seconds).ToString() + "!";
    }

    public void RestartGame()
    { 

        CoinRotation.coinList.Clear();
        CoinRotation.totalCoins = 0;
        CoinRotation.currentScore = 0;
        CharacterMechanics.seconds = 0;
        CharacterMechanics.minutes = 0;
        SceneManager.LoadScene("MainMenu");
  
    }
}
