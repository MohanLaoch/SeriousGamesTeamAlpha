using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MindfulnessManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] DialogueClips;

    public Slider dialogueSlider;

    public AudioMixer audioMixer;

    public GameObject canvas;
    private const string DIALOGUE_VOLUME = "DialogueVolume";
    private const string MEDITATION_THEME = "Meditation Theme";
    public LevelLoader loader;
    // Start is called before the first frame update

    private void Awake()
    {
        dialogueSlider.onValueChanged.AddListener(SetDialogueVolume);
        dialogueSlider.value = PlayerPrefs.GetFloat(DIALOGUE_VOLUME, 0.5f);
        SetDialogueVolume(dialogueSlider.value);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioManager.instance.Play(MEDITATION_THEME);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SaveValues()
    {
        PlayerPrefs.SetFloat(DIALOGUE_VOLUME, dialogueSlider.value);
    }

    public void OpenCanvas()
    {
        audioSource.Pause();
        Time.timeScale = 0;
        canvas.SetActive(true);
        
    }

    public void CloseCanvas()
    {
        Time.timeScale = 1;
        SaveValues();
        canvas.SetActive(false);
        audioSource.UnPause();
        
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        StopAudio();
        CloseCanvas();
        SaveValues();
        AudioManager.instance.Stop(MEDITATION_THEME);
        loader.LoadLevel("MainMenu");
        
    }
    public void SetDialogueVolume(float volume)
    {
        audioMixer.SetFloat(DIALOGUE_VOLUME, Mathf.Log10(volume) * 20);
    }
    
    public void StopAudio()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
    public void PlayAudio(int index)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = DialogueClips[index];
        
        audioSource.Play();
    }
}
