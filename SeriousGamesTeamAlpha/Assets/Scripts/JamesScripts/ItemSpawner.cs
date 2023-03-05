using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ItemSpawner : MonoBehaviour
{
    public float secondsToWait = 1;

    public GameObject animationObject;
    public Animator animator;

    public GameObject[] foodItems;

    public void InsantiateItem()
    {
        int randomIndex = Random.Range(0, foodItems.Length);

        Vector3 spawnPos = this.transform.position;

        GameObject newItem = Instantiate(foodItems[randomIndex], spawnPos, Quaternion.identity, transform.parent);
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
        animationObject.SetActive(true);
        yield return new WaitForSeconds(secondsToWait);
        animationObject.SetActive(false);
    }

}
