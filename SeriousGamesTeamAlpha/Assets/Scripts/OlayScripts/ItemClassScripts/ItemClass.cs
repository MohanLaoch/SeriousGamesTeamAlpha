using System;
using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class ItemClass : MonoBehaviour
    {
        public float spawnChance;

        //More frequent the farther you go if yes or less common the farther you go
        public bool inverseSpawnRate;
        //Bit of an explanation as to why I made this a class instead of a scriptableObject. I thought about making it a scriptable Object
        //it would've been neater and more presentable....however I thought that I wouldn't be able to give each SO it's own scripts
        //as i would need to somehow reference each script on a global level and somehow use MonoBehaviour functions.
        //I might change this down the line or something, or someone reading this figures it out ಥ_ಥ
        protected void OnTriggerEnter2D(Collider2D col)
        {
            //checks to see if it collides with the player, when it does activates a function and then destroys. This applies to all classes so I don't have to individually write each one..

            if(RunningGameManager.instance.gameState == GameState.Finished)
                return;
            
            if (col.gameObject.CompareTag("Player"))
            {
                if (RunningGameManager.instance.gameState == GameState.Boosted)
                {
                    Destroy(gameObject);
                }

                else
                {
                    OnPlayerCollide();
                    Destroy(gameObject);
                }
                
                
            }
        }

        protected void OnCollisionEnter2D(Collision2D col)
        {
            //checks to see if it collides with the player, when it does activates a function and then destroys. This applies to all classes so I don't have to individually write each one..

            if (RunningGameManager.instance.gameState == GameState.Boosted)
            {
                Destroy(gameObject);
            }

            else
            {
                OnPlayerCollide();
                Destroy(gameObject);
            }
        }
        

        public virtual void OnPlayerCollide()
        {
            
        }
    }
}