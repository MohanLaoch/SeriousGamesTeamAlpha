using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public float parallaxSpeed = 0.5f;
    private Material parallaxMaterial;
    // Start is called before the first frame update
    void Start()
    {
        parallaxMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        
        float x = PlayerMovement.instance.moveInput.x * (PlayerMovement.instance.speed / PlayerMovement.instance.walkSpeedRef) * parallaxSpeed * Time.deltaTime;
        Mathf.Clamp(x, 0, float.MaxValue);
        parallaxMaterial.mainTextureOffset += new Vector2(x, 0);
    }
}
