using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class PowerUp : MonoBehaviour
    {
        public PowerUps powerType;
        private EventHandler<HitEventArgs> Hit;

        void Awake()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            var powerUpManager = referenceManager.powerUpManager;
            Hit += powerUpManager.PowerUp;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            var layer = LayerMask.LayerToName(col.gameObject.layer);
            if (layer != "Player")
            {
                return;
            }
            if (Hit == null)
            {
                return; 
            }
            Hit(this, new HitEventArgs((int) powerType));
            gameObject.SetActive(false);
        }
    }
}