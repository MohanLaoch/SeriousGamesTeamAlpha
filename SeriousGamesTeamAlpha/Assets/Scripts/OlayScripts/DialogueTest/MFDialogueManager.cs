using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MFDialogueManager : MonoBehaviour
{
    private string dialogueString;
    public GameObject dialogueCanvas;
    private bool isDialoguePlaying;
    public Button continueButton;
    public TextMeshProUGUI text;
    
    

    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(NextLine);    
    }

    public void StartDialogue()
    {
        
    }
    
    
    public void NextLine()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
