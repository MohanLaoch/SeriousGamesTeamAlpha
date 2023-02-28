using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
   
    public class WaterItemClass : ItemClass
    {
        public float hydrationAmount;
        public override void OnPlayerCollide()
        {
            GameManager.instance.IncreaseHydration(hydrationAmount);
        }
    }
}