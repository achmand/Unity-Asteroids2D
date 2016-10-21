using UnityEngine;

namespace Assets.Scripts
{
    public sealed class DetectionField : MonoBehaviour
    {
        public float detectionSize;
        [HideInInspector] public bool detectedPlayer;
        [HideInInspector] public CircleCollider2D _circleCollider;

        void Awake()
        {
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.radius = 2;
        }

        public void IncreaseDetection(float increase)
        {
            detectionSize += increase;
            _circleCollider.radius = detectionSize;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            var layer = LayerMask.LayerToName(col.gameObject.layer);
            detectedPlayer = layer == "Player";
        }
    }
}
