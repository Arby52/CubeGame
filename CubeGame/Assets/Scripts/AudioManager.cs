using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public AudioMixerGroup MusicMixerGroup;
    public AudioMixerGroup SFXMixerGroup;
    
    AudioSource MusicSource;

    public AudioClip music;

    //Singleton stuff
    public static AudioManager Instance = null;

    public List<AudioSource> sfxList = new List<AudioSource>();

    private void Awake()
    {
        //Singleton setup
        if(Instance == null)
        {
            Instance = this;
        } else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {        
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.outputAudioMixerGroup = MusicMixerGroup;

        MusicSource.clip = music;
        MusicSource.loop = true;
        if (!MusicSource.isPlaying)
        {
            MusicSource.Play();
        }
    }

    private void Update()
    {
        for(int i = 0; i < sfxList.Count; i++)
        {
            if (sfxList[i] != null)
            {
                if (!sfxList[i].isPlaying)
                {
                    AudioSource referance = sfxList[i];
                    sfxList.Remove(referance);
                    Destroy(referance);
                }
            }
        }
    }

    //Only one sfx at a time rn. 
    public void Play(AudioClip _audio)
    {
        GameObject sfxChild = new GameObject();        
        AudioSource sfx = sfxChild.AddComponent<AudioSource>();
        sfx.outputAudioMixerGroup = SFXMixerGroup;
        sfx.clip = _audio;
        sfx.Play();
        sfxList.Add(sfx);
    }
}
