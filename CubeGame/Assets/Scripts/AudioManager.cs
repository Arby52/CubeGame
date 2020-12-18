using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public AudioMixerGroup MusicMixerGroup;
    public AudioMixerGroup SFXMixerGroup;

    AudioSource SFXSource;
    AudioSource MusicSource;

    public AudioClip music;

    //Singleton stuff
    public static AudioManager Instance = null;

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
        SFXSource = gameObject.AddComponent<AudioSource>();
        SFXSource.outputAudioMixerGroup = SFXMixerGroup;
        MusicSource = gameObject.AddComponent<AudioSource>();
        MusicSource.outputAudioMixerGroup = MusicMixerGroup;

        MusicSource.clip = music;
        MusicSource.loop = true;
        if (!MusicSource.isPlaying)
        {
            MusicSource.Play();
        }
    }

    //Only one sfx at a time rn. 
    public void Play(AudioClip _audio)
    {
        SFXSource.clip = _audio;
        SFXSource.Play();
    }
}
