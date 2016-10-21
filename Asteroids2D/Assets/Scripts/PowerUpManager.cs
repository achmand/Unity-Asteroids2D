using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class HitEventArgs : EventArgs
    {
        public HitEventArgs(int current)
        {
            Current = current;
        }
        public int Current { get; private set; }
    }


    public sealed class PowerUpManager : MonoBehaviour
    {
        private PlayerManager playerManager;
        private PlayerWeapon playerWeapon;
        private PowerUpRepository powerUpRepository;

        private void FindReferences()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            playerManager = referenceManager.playerManager;
            playerWeapon = playerManager.playerWeapon;
            powerUpRepository = referenceManager.powerUpRepository;
        }

        void Awake()
        {
            FindReferences();
        }

        public void PowerUp(object sender, HitEventArgs e)
        {
            var powerUp = (PowerUps)e.Current; 
            HandlePowerUp(powerUp);
        }

        private void HandlePowerUp(PowerUps powerUpType)
        {
            var powerUpOption = powerUpRepository.powerUpDictionary[powerUpType];
            var powerUp = GetPowerUp(powerUpOption);
            StartCoroutine(powerUp);
        }

        private IEnumerator GetPowerUp(PowerUpOptions powerUpOption)
        {
            var powerUpType = powerUpOption.powerUpType;
            var lifeTime = powerUpOption.lifeTime;

            switch (powerUpType)
            {
                case PowerUps.HellFire:
                    return HellFire(lifeTime);
                case PowerUps.Health:
                    return HealthUp(lifeTime);
                case PowerUps.Energy:
                    return EnergyUp(lifeTime);
                case PowerUps.SonarField:
                    return SonarField(lifeTime);
            }

            return null;
        }

        private IEnumerator HellFire(float lifeTime)
        {
            playerManager.poweredUp = true;
            var coolDownTimerRate = playerWeapon.coolDownTimer.rate;
            playerWeapon.nextFire = .1f;
            yield return new WaitForSeconds(lifeTime);
            playerWeapon.nextFire = coolDownTimerRate;
            playerManager.poweredUp = false;
        }

        private IEnumerator HealthUp(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            playerManager.AddHealth(1);
        }

        private IEnumerator EnergyUp(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            playerManager.AddEnergy(.2f);
        }

        private IEnumerator SonarField(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            //playerManager.AddEnergy(.2f);
        }
    }
}
