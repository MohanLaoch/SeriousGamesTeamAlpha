using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    Vector2 difference = Vector2.zero;

    BoxCollider2D colldier;


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
        
        yield return new WaitForSeconds(0.01f);
        colldier.isTrigger = false;
    }
}
