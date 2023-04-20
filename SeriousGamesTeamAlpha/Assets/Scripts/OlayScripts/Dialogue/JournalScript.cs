using System;
using UnityEngine;

namespace OlayScripts.Dialogue
{
    public class JournalScript : MonoBehaviour
    {
        public GameObject Journal;
        private void Start()
        {
            Journal.SetActive(false);
            DialogueManager.instance.journalEvent += OnJournalOpen;
        }


        void OnJournalOpen()
        {
            Journal.SetActive(true);
        }

        public void JournalInput(string input)
        {
            if(String.IsNullOrEmpty(input))
                return;
            Journal.SetActive(false);
            Debug.Log("made it to the end");
        }
        
    }
}