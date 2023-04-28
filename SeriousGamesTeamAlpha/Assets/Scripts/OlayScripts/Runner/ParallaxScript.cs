using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public float parallaxSpeed = 0.5f;
    private Material parallaxMaterial;

    public bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        parallaxMaterial = GetComponent<Renderer>().material;
    }

    // In Late update because we want to parallax after the player has moved
    void LateUpdate()
    {

        if (!isGround)
        {
            //gets the player position and creates the offset based on the player's speed and if they're boosting / not
            float x = 2 * RunningGameManager.instance.GameSpeed * parallaxSpeed * Time.deltaTime;
            //prevents it from going over the 32 bit integer limit
            x = Mathf.Clamp(x, 0, float.MaxValue);
            //moves offset based on the x value
            parallaxMaterial.mainTextureOffset += new Vector2(x, 0);
        }

        else
        {
            float x = ObstaclePooler.SharedInstance.speed * Time.deltaTime * parallaxSpeed;
            
            parallaxMaterial.mainTextureOffset += new Vector2(x, 0);
            
        }
    }
    
}
