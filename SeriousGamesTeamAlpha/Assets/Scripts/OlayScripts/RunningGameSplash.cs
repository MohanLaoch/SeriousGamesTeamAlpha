using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGameSplash : MonoBehaviour
{
    private bool anyKeyPressed;
    public GameObject informationPanel;
    public GameObject keyPanel;
    private int stateId;
    private void Awake()
    {
        Time.timeScale = 0;
        
    }
    
    

    // Start is called before the first frame update
    void Start()
    {
        OnInformationRead(0);
    }

    public void OnInformationRead(int state)
    {
        switch (state)
        {
            case 0:
                informationPanel.SetActive(true);
                keyPanel.SetActive(false);
                Time.timeScale = 0;
                PlayerMovement.instance.canMove = false;
                break;
            case 1:
                informationPanel.SetActive(false);
                keyPanel.SetActive(true);
                break;
            case 2:
                keyPanel.SetActive(false);
                informationPanel.SetActive(false);
                gameObject.SetActive(false);
                PlayerMovement.instance.canMove = true;
                Time.timeScale = 1;
                break;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        anyKeyPressed = Input.anyKeyDown;

        if (anyKeyPressed)
        {
            OnInformationRead(stateId += 1);
        }
    }
}
