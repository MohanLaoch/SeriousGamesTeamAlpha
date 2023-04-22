using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField] private Slider mainSlider, sfxSlider, musicSlider;


    private void Awake()
    {
            
    }

    private void Start()
    {
        mainSlider.onValueChanged.AddListener(SetMainVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        
        
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MasterVolume", mainSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", sfxSlider.value);
    }

    public void SetMainVolume (float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("SoundVolume", Mathf.Log(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MusicVolume",Mathf.Log10(volume) * 20);
    }
}
