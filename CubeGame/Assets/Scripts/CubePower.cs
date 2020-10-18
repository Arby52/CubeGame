using UnityEngine;

[CreateAssetMenu(fileName = "power", menuName = "CubePowers", order = 1)]
public class CubePower : ScriptableObject
{
    public string powerName;
    public string powerDescription;

    public Material powerMaterial;
    public PowerStates powerState;
}
