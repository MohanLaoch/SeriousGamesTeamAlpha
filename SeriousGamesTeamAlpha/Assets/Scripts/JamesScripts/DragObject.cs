using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    Vector2 difference = Vector2.zero;

    public Vector3 itemSpawnerPos;
    BoxCollider2D colldier;

    public int checkCount;

    public int collisionCount;
    

    private void Awake()
    {
        colldier = this.gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;

        colldier.enabled = false;
        colldier.isTrigger = false;
        

    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void OnMouseUp()
    {
        colldier.enabled = true;
        colldier.isTrigger = true;

        StartCoroutine(Switch());
    }
    
    IEnumerator Switch()
    {

        yield return new WaitForFixedUpdate();
        if (checkCount == 0)
        {
            colldier.isTrigger = false;
            transform.position = itemSpawnerPos;
            checkCount = 0;
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out CheckItem checkItem);

        if (checkItem)
        {
            if (checkCount > 0)
                return;
            checkItem.OnCollision(gameObject);
            checkCount++;
        }

        else
        {
            transform.position = itemSpawnerPos;
            colldier.isTrigger = false;
        }

       
    }
}
