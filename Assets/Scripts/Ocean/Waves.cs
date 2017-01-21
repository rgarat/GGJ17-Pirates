using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Pirates.Ocean
{


    [Serializable]
    public class Waves
    {
        public enum WaveTipe
        {
            Regular,
            SingleWave,
        }


        public Vector3 source;
        public float speed;
        public float scale;
       // [NonSerialized]
        public float timeSinceStart;


        public float waveNumber;

        public float timeToLive;

        public float dampenFactor;

        public WaveTipe waveType;

        public float thing;

        public bool freeze = false;

        public float phase;

            //Sinus waves
        public float WaveValue(Vector3 position)
        {

            float distance = (position - source).magnitude;

            //Using only x or z will produce straight waves
            //Using only y will produce an up/down movement
            //x + y + z rolling waves
            //x * z produces a moving sea without rolling waves

            if (waveType == WaveTipe.Regular)
            {
                return Mathf.Sin(timeSinceStart * speed + distance * waveNumber) * scale;
            }
            else
            {

                float wavelength = 2 * Mathf.PI / waveNumber;
                float realScale = scale;
                var waveCenter = timeSinceStart * speed;
                if (Mathf.Abs(distance - waveCenter) > wavelength / 4)
                {
                    realScale = 0;
                }

                return Mathf.Sin(timeSinceStart * speed + distance * waveNumber + phase) * realScale;
            }


        }

        public bool Update(float delta)
        {
            if (!freeze)
            {
                timeSinceStart += delta;
            }

            return timeToLive < timeSinceStart;
        }
    }
}