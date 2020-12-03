using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinRotation : MonoBehaviour
{
    //Used to know how many coins exist.
    public static List<CoinRotation> coinList = new List<CoinRotation>();

    private void Start()
    {
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
            Destroy(this.gameObject);
        }        
    }
}
