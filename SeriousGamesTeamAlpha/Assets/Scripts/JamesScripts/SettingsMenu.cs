using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField] private Slider mainSlider, sfxSlider, musicSlider, dialogueSlider;

    private const string MAIN_MENU_THEME = "Main Menu Theme";

    private const string MASTER_VOLUME = "MasterVolume";
    private const string SOUND_VOLUME = "SoundVolume";
    private const string MUSIC_VOLUME = "MusicVolume";
    private const string DIALOGUE_VOLUME = "DialogueVolume";
    private void Awake()
    {

        
    }

    private void Start()
    {
        
        AudioManager.instance.Play(MAIN_MENU_THEME);
        
        mainSlider.onValueChanged.AddListener(SetMainVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        dialogueSlider.onValueChanged.AddListener(SetDialogueVolume);


        if (PlayerPrefs.HasKey(MASTER_VOLUME))
        {
            mainSlider.value = PlayerPrefs.GetFloat(MASTER_VOLUME);
        }

        if (PlayerPrefs.HasKey(SOUND_VOLUME))
        {
            sfxSlider.value = PlayerPrefs.GetFloat(SOUND_VOLUME);
        }

        if (PlayerPrefs.HasKey(MUSIC_VOLUME))
        {
            musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME);
        }

        if (PlayerPrefs.HasKey(DIALOGUE_VOLUME))
        {
            dialogueSlider.value = PlayerPrefs.GetFloat(DIALOGUE_VOLUME);
        }
        
        
        
        SetMainVolume(mainSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);
        SetDialogueVolume(dialogueSlider.value);





    }

    public void SaveValues()
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME, mainSlider.value);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, musicSlider.value);
        PlayerPrefs.SetFloat(SOUND_VOLUME, sfxSlider.value);
        PlayerPrefs.SetFloat(DIALOGUE_VOLUME, dialogueSlider.value);
        
    }

    public void OnDisable()
    {
        AudioManager.instance.Stop(MAIN_MENU_THEME);
        SaveValues();
    }

    public void SetMainVolume (float volume)
    {
        
        audioMixer.SetFloat(MASTER_VOLUME, Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SOUND_VOLUME, Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
      
        audioMixer.SetFloat(MUSIC_VOLUME,Mathf.Log10(volume) * 20);
    }

    public void SetDialogueVolume(float volume)
    {
        audioMixer.SetFloat(DIALOGUE_VOLUME, Mathf.Log10(volume) * 20);
    }
}
