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
            for (var index = 0; index < waves.Count; index++)
            {
                var wave = waves[index];
                height += wave.WaveValue(pos);
            }

            pos.y = height;
            return pos;

        }

        public void Update()
        {
            var delta = Time.deltaTime;
            for (var index = 0; index < waves.Count; index++)
            {
                var wave = waves[index];
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


            Gizmos.color = Color.grey;

            var bounds = new Rect();
            bounds.min = new Vector2(0, 0);
            bounds.max = this.size;

            if (!Application.isPlaying)
            {
                Gizmos.DrawCube(new Vector3(this.size.x / 2.0f, 0, this.size.y / 2.0f), new Vector3(this.size.x, 0.1f, this.size.y));
            }




            Gizmos.color = oldColor;






            Gizmos.matrix = oldMatrix;




        }
    }
}