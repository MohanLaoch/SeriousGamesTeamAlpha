using System.Collections.Generic;
using Ink.Runtime;
using System.IO;
using Ink;
using UnityEngine;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    public DialogueVariables(TextAsset loadGlobalsJson)
    {
        Story globalVariablesStory = new Story(loadGlobalsJson.text);

        variables = new Dictionary<string, Ink.Runtime.Object>();

        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
                        
        }
    }
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += OnVariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= OnVariableChanged;
    }


    private void OnVariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach (KeyValuePair<string, Ink.Runtime.Object> kvp in variables)
        {
            story.variablesState.SetGlobal(kvp.Key, kvp.Value);
                        
        }
    }
}