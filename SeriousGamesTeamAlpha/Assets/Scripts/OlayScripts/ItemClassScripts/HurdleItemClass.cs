using System.Collections;
using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class HurdleItemClass : ItemClass
    {
        
        public float HitHydrationAmount;
        public override void OnPlayerCollide()
        {
            //checks the game state so we don't have infinite invisibility frames
            if (RunningGameManager.instance.gameState == GameState.Hit)
                return;
            AudioManager.instance.Play("Hit");
            RunningGameManager.instance.DecreaseHydration(HitHydrationAmount);
            RunningGameManager.instance.StartHurdleHit();
            
        }


        
    }
}