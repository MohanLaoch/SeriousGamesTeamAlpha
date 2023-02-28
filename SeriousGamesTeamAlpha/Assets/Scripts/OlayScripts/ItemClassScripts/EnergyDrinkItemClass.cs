using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
    public class EnergyDrinkItemClass : ItemClass
    {
        public float EnergyHydrationAmount;
        public override void OnPlayerCollide()
        {
            GameManager.instance.IncreaseHydration(EnergyHydrationAmount);
            GameManager.instance.StartBoost();
        }
    }
}