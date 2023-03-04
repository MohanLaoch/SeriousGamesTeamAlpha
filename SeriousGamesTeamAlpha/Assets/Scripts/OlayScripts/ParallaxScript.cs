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

    // In Late update because we want to parallax after the player has moved
    void LateUpdate()
    {
        
        //gets the player position and creates the offset based on the player's speed and if they're boosting / not
        float x = PlayerMovement.instance.moveInput.x * (PlayerMovement.instance.speed / PlayerMovement.instance.walkSpeedRef) * parallaxSpeed * Time.deltaTime;
        //prevents it from going over the 32 bit integer limit
        x = Mathf.Clamp(x, 0, float.MaxValue);
        //moves offset based on the x value
        parallaxMaterial.mainTextureOffset += new Vector2(x, 0);
    }
}
