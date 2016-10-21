using UnityEngine;

namespace Assets.Scripts
{
    public sealed class WhiteRoseSaucer : SaucerBase
    {
        private DetectionField detectionField;
        private PlayerController playerController;
        private bool foundReferences;
        private Rigidbody2D _rigidbody2D;

        private void FindReferences()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            playerController = referenceManager.playerManager.playerController;
            detectionField = GetComponentInChildren<DetectionField>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            foundReferences = true;
        }

        void Awake()
        {
            FindReferences();
        }

        void OnEnable()
        {
            var angles = transform.localEulerAngles;
            angles = new Vector3(angles.x, angles.y, Random.Range(-360, 360));
            transform.localEulerAngles = angles;
        }

        void Update()
        {
            if (!foundReferences)
            {
                return;   
            }

        }

        public override void AttackIntruders()
        {
        }
    }
}
