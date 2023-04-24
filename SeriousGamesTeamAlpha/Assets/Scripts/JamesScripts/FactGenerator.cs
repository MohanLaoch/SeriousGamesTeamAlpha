using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FactGenerator : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] facts;

    public TMP_Text factText;

    public Sprite[] factSprites;

    public Image factImage;

    private void Awake()
    {
        GenerateFact();
    }

    public void GenerateFact()
    {
        int randomIndex = Random.Range(0, facts.Length);

        string randomFact = facts[randomIndex];

        Sprite randomSprite = factSprites[randomIndex];

        factText.text = randomFact;

        factImage.sprite = randomSprite;
    }

}