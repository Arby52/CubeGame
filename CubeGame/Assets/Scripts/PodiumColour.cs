using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerStates
{
    none,
    speed
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
}
