using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class BoatHud : MonoBehaviour
    {
        public Slider healthSlider;


        public Life life;
        public Fire fire;


        public void LateUpdate()
        {
            healthSlider.value = ((float)life.life) / life.maxLife;
        }
    }
}