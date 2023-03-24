using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FactGenerator : MonoBehaviour
{
    [TextArea(3,10)]
    public string[] facts;

    public TMP_Text factText;

    private void Awake()
    {
        GenerateFact();
    }

    public void GenerateFact()
    {
        string randomFact = facts[Random.Range(0, facts.Length)];

        factText.text = randomFact;
    }

}
