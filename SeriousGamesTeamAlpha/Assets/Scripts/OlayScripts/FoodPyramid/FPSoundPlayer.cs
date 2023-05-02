using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSoundPlayer : MonoBehaviour
{

    private const string FOOD_PYRAMID_THEME = "Food Pyramid Theme";
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play(FOOD_PYRAMID_THEME);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnQuit()
    {
        AudioManager.instance.Stop(FOOD_PYRAMID_THEME);
    }
    private void OnDisable()
    {
        
    }
}
