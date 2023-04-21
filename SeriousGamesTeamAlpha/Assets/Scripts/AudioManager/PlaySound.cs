using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void IconHover()
    {
        FindObjectOfType<AudioManager>().Play("IconHover");
    }

    public void ButtonClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void StopRunnerMusic()
    {
        FindObjectOfType<AudioManager>().Stop("Main Runner Game Music");
    }
}
