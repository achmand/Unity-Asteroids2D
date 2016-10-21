using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public enum AsteroidSize
    {
        Small,
        Medium,
        Large
    }

    public sealed class BaseAsteroid : MonoBehaviour
    {
        public AsteroidSize asteroidSize;
        public float moveRate;

        [HideInInspector]
        public AsteroidTypes asteroidType;
        [HideInInspector]
        public SpriteRenderer _spriteRenderer;

        private Rigidbody2D _rigidbody2D;
        private AsteroidSpawner asteroidSpawner;
        private bool foundReferences;
        private bool readyForce;

        private EventHandler<HitEventArgs> Hit;
        private EventHandler<HitEventArgs> ApplyDamage;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            asteroidSpawner = referenceManager.asteroidSpawner;
            
            var gameManager = referenceManager.gameManager;
            var playerManager = referenceManager.playerManager;
            Hit += gameManager.AddScore;
            ApplyDamage += playerManager.EnergyLoss;
        }

        void OnEnable()
        {
            var angles = transform.localEulerAngles;
            angles = new Vector3(angles.x, angles.y, Random.Range(-360, 360));
            transform.localEulerAngles = angles;
            foundReferences = true;
        }

        void OnDisable()
        {
            foundReferences = false;
            readyForce = false;
        }

        void Update()
        {
            if (!foundReferences)
            {
                return;
            }
            if (!readyForce)
            {
                var force = transform.up * moveRate;
                _rigidbody2D.AddForce(force);
                readyForce = true;
            }
            transform.Rotate(0, 0, moveRate / 2);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            var layer = LayerMask.LayerToName(col.gameObject.layer);
            if (layer == "Player Projectile")
            {
                Hit(this, new HitEventArgs((int) asteroidSize));
                HandleDamage();
            }
            if (layer == "Player")
            {
                ApplyDamage(this, new HitEventArgs((int) asteroidSize));
                HandleDamage();
            }
        }

        private void HandleDamage()
        {
            var explosion = ProjectilePooler.currentInstance.GetExplosionParticleSystem();
            var curreentPosition = transform.position;

            if (explosion != null)
            {
                explosion.transform.position = curreentPosition;
                explosion.gameObject.SetActive(true);
            }

            gameObject.SetActive(false);
            if (asteroidSize == AsteroidSize.Small)
            {
                return;
            }
            asteroidSpawner.HandleChildAsteroid(asteroidType, asteroidSize, curreentPosition);
        }
    }
}