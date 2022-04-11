using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour {
    public AudioSettings audioSettings;

    public bool ambientMusicOn;
    public Sound ambientMusic;
    public Sound[] sounds;

    private AudioSource audioPlaying;
    private float defaultVolume;
    private float defaultAmbient;
    private float lowerVolume;

    private void Awake() {
        defaultVolume = audioSettings.GetInstance().volume;
        defaultAmbient = defaultVolume - defaultVolume * (2f/3f);
        lowerVolume = defaultVolume - defaultVolume * (3f/4f);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = defaultVolume;
        }

        if (ambientMusicOn) {
            ambientMusic.source = gameObject.AddComponent<AudioSource>();
            ambientMusic.source.volume = defaultAmbient;
            ambientMusic.source.clip = ambientMusic.clip;
            ambientMusic.source.loop = true;
            ambientMusic.source.Play();
        }
    }

    private void Update() {
        defaultVolume = audioSettings.GetInstance().volume;
        defaultAmbient = defaultVolume - defaultVolume * (2f/3f);
        lowerVolume = defaultVolume - defaultVolume * (3f/4f);

        ambientMusic.source.volume = defaultAmbient;
        if (audioPlaying != null) {
            if (!audioPlaying.isPlaying) {
                ambientMusic.source.volume = defaultAmbient;
            }
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

        ambientMusic.source.volume = lowerVolume;
        audioPlaying = s.source;
    }
}