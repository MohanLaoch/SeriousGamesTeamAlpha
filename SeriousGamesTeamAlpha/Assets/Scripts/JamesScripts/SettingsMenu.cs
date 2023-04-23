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
        
        mainSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.3f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        
        Debug.Log(mainSlider.value);
        Debug.Log(sfxSlider.value);
        Debug.Log(musicSlider.value);
        
        SetMainVolume(mainSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);
        
        
        
        
    }

    public void SaveValues()
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
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MusicVolume",Mathf.Log10(volume) * 20);
    }
}
