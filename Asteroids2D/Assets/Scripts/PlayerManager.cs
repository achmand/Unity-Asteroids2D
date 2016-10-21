using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PlayerManager : MonoBehaviour
    {
        public int currentLives = 3;

        public bool IsAlive
        {
            get { return currentEnergy > 0; }
        }

        public bool IsDead
        {
            get { return currentLives <= 0; }
        }

        [HideInInspector] public PlayerWeapon playerWeapon;
        [HideInInspector] public PlayerController playerController;
        [HideInInspector] public bool poweredUp;

        private float currentEnergy = 1f;
        private UiManager uiManager;
        private bool initialized;

        private void FindReferences()
        {
            playerWeapon = GetComponent<PlayerWeapon>();
            playerController = GetComponent<PlayerController>();
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            uiManager = referenceManager.uiManager;
        }

        public void Intialize()
        {
            if (initialized)
            {
                return;
            }

            FindReferences();
            uiManager.SetHealth(currentLives);
            initialized = true;
        }

        public void EnergyLoss(object sender, HitEventArgs e)
        {
            var asteroidSize = (AsteroidSize)e.Current;
            HandleEnergyLoss(asteroidSize);
        }

        private void HandleEnergyLoss(AsteroidSize asteroidSize)
        {
            var energyLost = PointDamageCalculator.GetEnergyLost(asteroidSize);
            currentEnergy -= energyLost;
            if (currentEnergy <= 0)
            {
                currentLives--;
                uiManager.SetHealth(currentLives);
                gameObject.SetActive(false);
            }
            uiManager.SetEnergyBar(-energyLost);
        }

        public void AddHealth(int lives)
        {
            currentLives += lives;
        }

        public void AddEnergy(float energyAdded)
        {
            currentEnergy += energyAdded;
            if (currentEnergy > 1f)
            {
                currentEnergy = 1f;
            }
        }
    }
}
