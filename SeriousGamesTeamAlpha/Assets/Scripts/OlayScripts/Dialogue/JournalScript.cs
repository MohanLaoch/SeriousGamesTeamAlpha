using System;
using UnityEngine;

namespace OlayScripts.Dialogue
{
    public class JournalScript : MonoBehaviour
    {
        
        public delegate void OnJournalComplete();

        public static OnJournalComplete onJournalCompleteEvent;
        public GameObject Journal;
        public TextAsset closingDialogueBad;
        public TextAsset closingDialogueGood;
        private void Start()
        {
            Journal.SetActive(false);
            DialogueManager.instance.journalEvent += OnJournalOpen;
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