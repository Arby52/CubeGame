using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string sceneName;

    public AudioClip levelEndAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            AudioManager.Instance.Play(levelEndAudio);
            SceneManager.LoadScene(sceneName);
            CoinRotation.coinList.Clear();
        }        
    }
}
