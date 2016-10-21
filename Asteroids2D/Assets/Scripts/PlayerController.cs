using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PlayerController : MonoBehaviour
    {
        public float defaultTopSpeed;
        public float accelerationRate;
        public float deccelerationRate;
        private float currentSpeed;

        private KeyboardInputManager keyboardInputManager;
        private ParticleSystem _particleSystem;
        private Vector3 lastTargetPosition;
        private float initialParticleSize;
        private float idleParticleSize;
        private float currentParticleSize;

        private bool IsIdle
        {
            get
            {
                return Math.Abs(keyboardInputManager.HorizontalAxis) < 0.1f && Math.Abs(keyboardInputManager.VeticalAxis) < 0.1f;
            }
        }

        public void FindReferences()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            keyboardInputManager = referenceManager.keyboardInputManager;
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            initialParticleSize = _particleSystem.startSize;
            idleParticleSize = initialParticleSize/2;
            currentParticleSize = idleParticleSize;
        }

        void Awake()
        {
            FindReferences();
        }

        void Update()
        {
            HandlePlayerControl();
        }

        public void HandlePlayerControl()
        {
            HandleMovement();
            HandleRotation();
        }

        // TODO: Add Decelaration
        private void HandleMovement()
        {
            if (currentSpeed >= defaultTopSpeed)
            {
                currentSpeed = defaultTopSpeed;
            }

            var inputHorizontal = keyboardInputManager.HorizontalAxis;
            var inputVertical = keyboardInputManager.VeticalAxis;
            if (!IsIdle)
            {
                currentSpeed += accelerationRate;
                var x = inputHorizontal*currentSpeed*Time.deltaTime;
                var y = inputVertical*currentSpeed*Time.deltaTime;
                var targetPostion = new Vector3(x, y, 0);
                lastTargetPosition = targetPostion;
                transform.Translate(lastTargetPosition);

                if (currentParticleSize < initialParticleSize)
                {
                    currentParticleSize += accelerationRate;
                }
            }

            if (IsIdle)
            {
                if (currentSpeed >= 0f)
                {
                    currentSpeed -= deccelerationRate;
                }

                if (currentParticleSize > idleParticleSize)
                {
                    currentParticleSize -= accelerationRate;
                }
            }

            _particleSystem.startSize = currentParticleSize;
        }

        private void HandleRotation()
        {
            var positionOnScreen = Camera.main.WorldToViewportPoint(transform.localPosition);
            var mouseOnScreen = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if ((Math.Abs(positionOnScreen.x - mouseOnScreen.x) <0.05f) && (Math.Abs(positionOnScreen.y - mouseOnScreen.y) < 0.05f))
            {
                return;
            }

            var angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
            transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));
        }

        private float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }
    }
}
