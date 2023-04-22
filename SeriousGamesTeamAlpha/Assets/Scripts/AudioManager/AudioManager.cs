using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    public AudioMixerGroup soundGroup;
    public AudioMixerGroup musicGroup;
    
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            switch (s.SoundType)
            {
                case SoundType.SFX:
                    s.source.outputAudioMixerGroup = soundGroup;
                    break;
                case SoundType.Music:
                    s.source.outputAudioMixerGroup = musicGroup;
                    break;
            }
        }
    }

    private void Start()
    {
        //Play("BackgroundTheme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void PlaySoundAtCertainPitch(string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if (CheckIfPlaying(name))
            {
                s.pitch = value;
            }

            else
            {
                s.source.Play();
            }
            
            
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            return;
        }
        
        s.source.Stop();
    }

    public void PlayAtLocation(string name, Vector3 position)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
            return;
        AudioSource.PlayClipAtPoint(s.clip, position);
    }

    public bool CheckIfPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        return s.source.isPlaying;
    }
    
    

}



/* 
 * Play Sound
 * FindObjectOfType<AudioManager>().Play("soundname");
 * 
 * Stop Sound
 * FindObjectOfType<AudioManager>().Stop("soundname");
 */
