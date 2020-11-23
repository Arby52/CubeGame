using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerStates
{
    none,
    speed,
    jumpHeight,
    sticky
}

[ExecuteAlways]
public class PodiumColour : MonoBehaviour
{
    public CubePower podiumPower;
    [HideInInspector]
    public PowerStates powerState;
        
    void Update()
    {
        if(podiumPower != null)
        {
            GetComponent<MeshRenderer>().material = podiumPower.powerMaterial;
            powerState = podiumPower.powerState;
        }
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterMechanics>().ChangeColour(podiumPower.powerMaterial, podiumPower.powerState);

        }        
    }
}
