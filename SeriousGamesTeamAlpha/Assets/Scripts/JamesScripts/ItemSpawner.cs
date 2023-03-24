using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class ItemSpawner : MonoBehaviour
{
    [Header("Animation")]

    public float secondsToWait = 1;

    public GameObject colourScreen;
    public Animator animator;

    [Header("Score")]
    public int score;

    public TMP_Text scoreText;


    [Header("Food")]

    public GameObject[] foodItems;

    public TMP_Text currentFoodText;

    public string[] foodItemsText;

    public void Awake()
    {
        score = 0;
        scoreText.text = "Current Streak:" + " " + score.ToString();
        InstantiateItem();
    }

    public void InstantiateItem()
    {
        int randomIndex = Random.Range(0, foodItems.Length);

        Vector3 spawnPos = this.transform.position;

        GameObject newItem = Instantiate(foodItems[randomIndex], spawnPos, Quaternion.identity);

        string foodText = foodItemsText[randomIndex];

        currentFoodText.text = foodText;
    }

    public void GreenAnimation()
    {
        StartCoroutine(Switch());
        animator.Play("Green");
    }

    public void RedAnimation()
    {
        StartCoroutine(Switch());
        animator.Play("Red");
    }

    IEnumerator Switch()
    {
        colourScreen.SetActive(true);
        yield return new WaitForSeconds(secondsToWait);
        colourScreen.SetActive(false);
    }

}
