using UnityEngine;

namespace OlayScripts.ItemClassScripts
{
   
    public class WaterItemClass : ItemClass
    {
        public float hydrationAmount;
        
        //overrides the parent class function allowing us to make our own functions
        //Linked to the GameManager class as I needed a reference to the slider.
        public override void OnPlayerCollide()
        {
            GameManager.instance.IncreaseHydration(hydrationAmount);
        }
    }
}