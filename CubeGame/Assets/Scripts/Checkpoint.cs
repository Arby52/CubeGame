using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    static List<Checkpoint> allCheckpoints = new List<Checkpoint>();

    MeshRenderer meshRenderer;
    public Material notActiveMat;
    public Material activeMat;

    public GameObject spawnPos;

    public AudioClip checkAudio;

    bool active = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = notActiveMat;
        allCheckpoints.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<CharacterMechanics>().SetCheckpoint(spawnPos.transform.position);
                AudioManager.Instance.Play(checkAudio);
                Activate();
            }
        }
    }

    public void Activate()
    {
        foreach(var c in allCheckpoints)
        {
            c.Deactivate();
        }
        active = true;
        meshRenderer.material = activeMat;
    }

    public void Deactivate()
    {
        active = false;
        meshRenderer.material = notActiveMat;
    }

    private void OnDestroy()
    {
        allCheckpoints.Remove(this);
    }
}
