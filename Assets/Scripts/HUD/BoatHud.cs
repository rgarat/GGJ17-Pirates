using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class BoatHud : MonoBehaviour
    {
        public Slider healthSlider;
        public HudCannonBall[] cannonBalls;

        public Life life;
        public Fire fire;

        int oldShootsAvailable;

        public void LateUpdate()
        {
            healthSlider.value = ((float)life.life) / life.maxLife;
            for (int i = 0; i < cannonBalls.Length; i++)
            {
                var hudCannonBall = cannonBalls[i];
                if (i < fire.shoots)
                {
                    hudCannonBall.Show(i >= oldShootsAvailable);
                }
                else
                {
                    hudCannonBall.Hide();
                }
            }

            oldShootsAvailable = fire.shoots;
        }
    }
}