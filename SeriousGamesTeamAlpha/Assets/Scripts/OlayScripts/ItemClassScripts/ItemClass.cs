using System;
using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class ItemClass : MonoBehaviour
    {
        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                OnPlayerCollide();
                Destroy(gameObject);
            }
        }

        protected void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
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