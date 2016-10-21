using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PowerUpRepository : MonoBehaviour
    {
        private PowerUpOptions[] powerUpOptions;
        public Dictionary<PowerUps, PowerUpOptions> powerUpDictionary;

        void Awake()
        {
            powerUpOptions = GetComponentsInChildren<PowerUpOptions>();
            powerUpDictionary = new Dictionary<PowerUps, PowerUpOptions>();
            for (int i = 0; i < powerUpOptions.Length; i++)
            {
                var powerUpOption = powerUpOptions[i];
                powerUpDictionary.Add(powerUpOption.powerUpType, powerUpOption);
            }
        }
    }
}
