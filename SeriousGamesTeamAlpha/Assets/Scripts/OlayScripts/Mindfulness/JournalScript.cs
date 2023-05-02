using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OlayScripts.Dialogue
{
    public class JournalScript : MonoBehaviour
    {
        
        public delegate void OnJournalComplete();

        public static OnJournalComplete onJournalCompleteEvent;
        public GameObject Journal;
        public TextAsset closingDialogueBad;
        public TextAsset closingDialogueGood;
        private bool wasFocused;
        public TMP_InputField inputField;
        private bool hasSubmitted;
        private void Start()
        {
            Journal.SetActive(false);
            DialogueManager.instance.journalEvent += OnJournalOpen;
        }


        private void Update()
        {
            if(hasSubmitted)
                return;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                JournalInput(inputField.text);
            }
            
        }

        void OnJournalOpen()
        {
            FindObjectOfType<MindfulnessManager>().StopAudio();
            Journal.SetActive(true);
            
        }

        public void JournalInput(string input)
        {
            if(String.IsNullOrEmpty(input))
                return;
            hasSubmitted = true;
            Journal.SetActive(false);
            Debug.Log("made it to the end");
            

            if (input.Length > 20)
            { 
                Debug.Log(input.Length);
                DialogueManager.instance.StartDialogue(closingDialogueGood);
            }

            else
            {
                DialogueManager.instance.StartDialogue(closingDialogueBad);
            }
            
            
        }
        
    }
}