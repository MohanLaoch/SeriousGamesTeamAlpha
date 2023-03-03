using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class EnergyDrinkItemClass : ItemClass
    {
        public float EnergyHydrationAmount;
        public override void OnPlayerCollide()
        {
            if(GameManager.instance.gameState == GameState.Boosted)
                return;
            GameManager.instance.DecreaseHydration(EnergyHydrationAmount);
            GameManager.instance.StartBoost();
        }
    }
}