using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Audio Settings")]
public class AudioSettings: ScriptableObject {
    public class Settings : ScriptableObject {
        private static Settings _instance;
        public float volume;
        
        public static Settings GetInstance(float initialVolume) {
            if (!_instance) {
                _instance = FindObjectOfType<Settings>();
            }

            if (!_instance)  {
                _instance = CreateInstance<Settings>();
                _instance.volume = initialVolume;
            }

            return _instance;
        }
    }

    [Range(0f, 1f)]
    public float initialVolume = 1f;

    public Settings GetInstance() {
        return Settings.GetInstance(initialVolume);
    }

    public void SetVolume(float newVolume) {
        Settings.GetInstance(initialVolume).volume = newVolume;
    }
}