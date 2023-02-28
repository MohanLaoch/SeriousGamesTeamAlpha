using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;

    private Vector3 currentVelocity;

    public float smoothTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 targetPos = GameManager.instance.player.transform.position;
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPos + offset, ref currentVelocity, smoothTime );
    }
}
