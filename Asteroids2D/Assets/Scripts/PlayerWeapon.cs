using UnityEngine;

namespace Assets.Scripts
{
    public enum WeaponType
    {
        Normal,
        Lightning,
        Laser,
        HyperForce
    }

    public sealed class PlayerWeapon : MonoBehaviour
    {
        private KeyboardInputManager keyboardInputManager;
        public GameObject bulletPrefab;
        public FirePoint firePointLeft;
        public FirePoint firePointRight;
        public float fireRate;
        public float nextFire;
        public CoolDownTimer coolDownTimer;

        private bool foundReferences;

        public void FindReferences()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            keyboardInputManager = referenceManager.keyboardInputManager;
            coolDownTimer = new CoolDownTimer
            {
                tickRate = fireRate,
            };
            foundReferences = true;
        }

        void Awake()
        {
            FindReferences();
        }

        void Update()
        {
            if (!foundReferences)
            {
                return;
            }
            coolDownTimer.rate = nextFire;
            coolDownTimer.ManualUpdate();
            var shootButton = keyboardInputManager.RightClickHold();
            if (!shootButton)
            {
                return;
            }
            FireShots();
        }

        private void FireShots()
        {
            var firePointRightTransform = firePointRight.gameObject.transform;
            var firePointLeftTransform = firePointLeft.gameObject.transform;
            var nextBulletL = ProjectilePooler.currentInstance.GetPooledObjectLeft();
            var nextBulletR = ProjectilePooler.currentInstance.GetPooledObjectRight();

            if (coolDownTimer.timerFinished)
            {
                if (nextBulletL != null)
                {
                    nextBulletL.gameObject.transform.position = firePointLeftTransform.position;
                    nextBulletL.gameObject.transform.rotation = firePointLeftTransform.rotation;
                    nextBulletL.SetActive(true);
                }
                if (nextBulletR != null)
                {
                    nextBulletR.gameObject.transform.position = firePointRightTransform.position;
                    nextBulletR.gameObject.transform.rotation = firePointRightTransform.rotation;
                    nextBulletR.SetActive(true);
                }
            }
        }
    }
}
