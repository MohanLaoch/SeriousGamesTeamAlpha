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

    public bool canSpawn;

    public void Awake()
    {
        score = 0;
        scoreText.text = score.ToString();
        canSpawn = true;
        InstantiateItem();
    }

    public void InstantiateItem()
    {
        if (canSpawn)
        {
            int randomIndex = Random.Range(0, foodItems.Length);

            Vector3 spawnPos = this.transform.position;

            GameObject newItem = Instantiate(foodItems[randomIndex], spawnPos, Quaternion.identity);

            canSpawn = false;

            string foodText = foodItemsText[randomIndex];

            currentFoodText.text = foodText;
        }

        StartCoroutine(SpawnTimer());
    }

    public void GreenAnimation()
    {
        StartCoroutine(Switch());
        animator.Play("Green");
        FindObjectOfType<AudioManager>().Play("CorrectFP");
    }

    public void RedAnimation()
    {
        StartCoroutine(Switch());
        animator.Play("Red");
        FindObjectOfType<AudioManager>().Play("IncorrectFP");
    }

    IEnumerator Switch()
    {
        colourScreen.SetActive(true);
        yield return new WaitForSeconds(secondsToWait);
        colourScreen.SetActive(false);
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.1f);
        canSpawn = true;
    }

}
