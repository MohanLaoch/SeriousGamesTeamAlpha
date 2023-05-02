using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void IconHover()
    {
        AudioManager.instance.Play("IconHover");
    }

    public void ButtonClick()
    {
        AudioManager.instance.Play("ButtonClick");
    }

    public void StopRunnerMusic()
    {
        FindObjectOfType<AudioManager>().Stop("Main Runner Game Music");
    }
}
