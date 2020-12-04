using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinRotation : MonoBehaviour
{
    //Used to know how many coins exist.
    public static List<CoinRotation> coinList = new List<CoinRotation>();
    public static int currentScore = 0;
    static TMP_Text coinText;

    private void Start()
    {
        coinText = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TMP_Text>();
        coinText.text = currentScore.ToString();
        coinList.Add(this);        
       
    }

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterMechanics>().coinAmount++;
            currentScore++;
            Destroy(this.gameObject);
            coinText.text = currentScore.ToString();
        }        
    }
}
