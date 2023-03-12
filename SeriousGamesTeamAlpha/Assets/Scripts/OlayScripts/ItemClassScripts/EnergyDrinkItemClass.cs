using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class EnergyDrinkItemClass : ItemClass
    {
        public float EnergyHydrationAmount;
        public override void OnPlayerCollide()
        {
            //checks the game state to make sure it's not on the boosted state so we don't boost again mid boost
            if(GameManager.instance.gameState == GameState.Boosted)
                return;
            //decrease hydration
            //GameManager.instance.DecreaseHydration(EnergyHydrationAmount);
            //activates boost from the GameManager as I only have a single frame to activate all these scripts 
            AudioManager.instance.Play("Fizzy");
            GameManager.instance.StartBoost();
        }
    }
}