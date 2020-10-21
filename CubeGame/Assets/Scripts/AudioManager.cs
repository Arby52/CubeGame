using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer MasterMixer;
    [SerializeField]
    AudioMixerGroup MusicMixerGroup;
    [SerializeField]
    AudioMixerGroup SFXMixerGroup;

    AudioSource SFXSource;
    AudioSource MusicSource;

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
    }

    //Only one sfx at a time rn. make it so it creates a new game object for the duration of the audio clip and then deletes it.
    public void Play(AudioClip _audio)
    {
        SFXSource.clip = _audio;
        SFXSource.Play();
    }
}
