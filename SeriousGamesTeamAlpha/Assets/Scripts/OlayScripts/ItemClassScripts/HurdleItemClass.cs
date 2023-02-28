using System.Collections;
using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class HurdleItemClass : ItemClass
    {
        public float invisibilityFrameTime = 2;
        public float HitHydrationAmount;
        public override void OnPlayerCollide()
        {
            if (GameManager.instance.gameState == GameState.Hit)
                return;
           
            StartCoroutine(PlayerHit(invisibilityFrameTime));
        }


        IEnumerator PlayerHit(float time)
        {
            GameManager.instance.SetGameState(GameState.Hit);
            GameManager.instance.DecreaseHydration(HitHydrationAmount);
            yield return new WaitForSeconds(time);
            GameManager.instance.SetGameState(GameState.Normal);
        }
    }
}