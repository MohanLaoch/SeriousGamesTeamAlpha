using System.Collections;
using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class HurdleItemClass : ItemClass
    {
        
        public float HitHydrationAmount;
        public override void OnPlayerCollide()
        {
            if (GameManager.instance.gameState == GameState.Hit)
                return;
            GameManager.instance.DecreaseHydration(HitHydrationAmount);
            GameManager.instance.StartHurdleHit();
            
        }


        
    }
}