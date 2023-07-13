using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
        [SerializeField] AudioSource _sfxAudioSource;
        public static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<AudioManager>();
                    if (_instance == null)
                    {
                        _instance = new GameObject("Audio Manager").AddComponent<AudioManager>();
                    }

                }
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance != null) 
                Destroy(this);
            DontDestroyOnLoad(this); 

        }
        public void PlaySound(AudioClip clip)
        {
            _sfxAudioSource.PlayOneShot(clip);
        }
    }
    
