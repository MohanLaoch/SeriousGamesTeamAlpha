using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public TextAsset starterDialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.instance.StartDialogue(starterDialogue);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
