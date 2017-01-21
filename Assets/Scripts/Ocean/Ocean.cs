using System.Collections.Generic;
using UnityEngine;

namespace Pirates.Ocean
{
    public class Ocean : MonoBehaviour
    {
        public Vector2 size;
        public float oceanMagnitude;

        public List<Waves> waves;

        public Vector3 GetWithHeight(Vector3 pos)
        {
            float height = 0;
            foreach (var wave in waves)
            {
                height+= wave.WaveValue(pos);
            }

            pos.y = height;
            return pos;

        }

        public void Update()
        {
            var delta = Time.deltaTime;
            foreach (var wave in waves)
            {
                wave.Update(delta);

                if (Input.GetMouseButtonDown(0) && wave.waveType == Waves.WaveTipe.SingleWave)
                {
                    wave.timeSinceStart = 0;
                }
            }


        }


        private void OnDrawGizmos()
        {
            var oldMatrix = Gizmos.matrix;
            var oldColor = Gizmos.color;
            Gizmos.matrix = this.transform.localToWorldMatrix;
            Gizmos.color = Color.red;

            foreach (var wave in waves)
            {
                Gizmos.DrawSphere(wave.source, 2);
            }

            Gizmos.color = oldColor;

            Gizmos.matrix = oldMatrix;
        }
    }
}