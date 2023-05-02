using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("instantiated");
    }

    public void IconHover()
    {
        AudioManager.instance.Play("IconHover");
        Debug.Log("sound played icon");
    }

    public void ButtonClick()
    {
        AudioManager.instance.Play("ButtonClick");
        Debug.Log("sound played click");
    }

    public void StopRunnerMusic()
    {
        FindObjectOfType<AudioManager>().Stop("Main Runner Game Music");
    }
}
