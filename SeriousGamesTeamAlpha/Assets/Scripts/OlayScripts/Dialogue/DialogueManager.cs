using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; set; }

    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private Animator actorSprite;
    [SerializeField] private TextMeshProUGUI actorName;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private GameObject dialogueCanvas;

    private Coroutine displayLineCoroutine;
    public Button ContinueButton;
    
    
    [Header("TExtAsset File")] [SerializeField]
    private TextAsset loadGlobalsJSON;
    private Story currentStory;

    private bool clicked;
    public bool isDialoguePlaying;  

    [SerializeField] private GameObject[] choices;
   
     private TextMeshProUGUI[] choicesText;
     private DialogueVariables _variables;
     private bool isAddingRichTextTag;


     
     
     
     private float delay;

     [Header("Dialogue Tags")] 
     private const string SPEAKER_TAG = "speaker";

     private const string EMOTION_TAG = "emotion";

     private const string POSE_TAG = "pose";
     
     private const string QUEST_TAG = "Quest";

     private const string WAIT_TAG = "delay";

     public delegate void OpenJournalEvent();

     public OpenJournalEvent journalEvent;
     
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else 
        {
            Destroy(gameObject);
        }

        _variables = new DialogueVariables(loadGlobalsJSON);
    }


    public void OnOpenJournalEvent()
    {
        if (journalEvent != null)
        {
            journalEvent();
        }
    }

    void ClickFunction()
    {
        clicked = true;
        
    }
    // Start is called before the first frame update
    void Start()
    {

        ContinueButton.onClick.AddListener(ClickFunction);
        choicesText = new TextMeshProUGUI[choices.Length];
        
        int index = 0;
        
        
        isDialoguePlaying = false;
        dialogueCanvas.SetActive(false);
        
        
        foreach (GameObject choice in choices)
        {
            
            choice.name = index.ToString();
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        
       
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        
        if(currentChoices.Count > choices.Length)
            return;

        
        int index = 0;
        
        

        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            ContinueButton.gameObject.SetActive(false);
            index++;
        }
        
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].SetActive(false);
        }
        
        

    }
    
    
    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        
        isDialoguePlaying = true;
        dialogueCanvas.SetActive(true);
        _variables.StartListening(currentStory);
        
        currentStory.BindExternalFunction("openJournal", OnOpenJournalEvent);

        
        ContinueDialogue();
    }

    private IEnumerator CloseDialogue()
    {
        yield return new WaitForSeconds(0.2f);
        _variables.StopListening(currentStory);
        isDialoguePlaying = false;
        dialogueCanvas.SetActive(false);
        dialogueText.text = String.Empty;
        actorName.text = "???";
        

    }

    // Update is called once per frame
    void Update()
    {
        if(!isDialoguePlaying)
            return;
        if (clicked && currentStory.currentChoices.Count == 0)
        {
            clicked = false;
            ContinueDialogue();
        }
    }


    
    void ContinueDialogue()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            if (currentStory.currentTags.Count > 0)
            {
                HandleTags(currentStory.currentTags);
            }
            
        }

        else
        {
            StartCoroutine(CloseDialogue());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        HideChoices();
        
        foreach (char letter in line.ToCharArray())
        {
            if (clicked)
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }

            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
            
            
        }
        
        
        DisplayChoices();
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
        
       
    }

    void HandleTags(List<string> currentTags)
    {
        
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be properly parsed" + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    actorName.text = tagValue;
                    break;
                case EMOTION_TAG:
                    if (actorSprite != null)
                    {
                        actorSprite.CrossFade(tagValue, 0.2f);
                    }
                    break;
                case POSE_TAG:
                    if (actorSprite != null)
                    {
                        actorSprite.CrossFade(tagValue, 0.2f );
                    }
                    
                    break;
                case WAIT_TAG:
                    if (float.TryParse(tagValue, out float index))
                    {
                        delay = index;
                        //Time.timeScale = 0;
                        StartCoroutine(TimerDelay());

                    }
                    break;

            }
        }
    }

    IEnumerator TimerDelay()
    {
        ContinueButton.gameObject.SetActive(false);
        yield return CoroutineUtilities.WaitForRealTime(delay);
        ContinueButton.gameObject.SetActive(true);
        clicked = true;
        //Time.timeScale = 1;
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        _variables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
            return null;
        return variableValue;
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueButton.gameObject.SetActive(true);
        ContinueDialogue();
    }
}

public static class CoroutineUtilities 
{
    public static IEnumerator WaitForRealTime(float delay){
        while(true){
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime){
                yield return 0;
            }
            break;
        }
    }
}