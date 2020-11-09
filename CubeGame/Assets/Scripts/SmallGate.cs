using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGate : MonoBehaviour
{
    float lastTime = 0;
    float delay = 0.5f;

    private void OnTriggerExit(Collider other)
    {
        if (Time.time - lastTime > delay)
        {
            lastTime = Time.time;
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<CharacterMechanics>().MediumSmallFlip();

            }
        }
    }
}
