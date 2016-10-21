using UnityEngine;

namespace Assets.Scripts
{
    public class BaseBullet : MonoBehaviour
    {
        public float bulletForce;
        private Rigidbody2D _rigidbody2D;
        void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            var force = transform.up*bulletForce;
            _rigidbody2D.AddForce(force);
        }

    }
}
