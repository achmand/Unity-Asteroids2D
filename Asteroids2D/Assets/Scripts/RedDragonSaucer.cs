using System;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SaucerBase : MonoBehaviour
    {
        public abstract void AttackIntruders();
    }

    public sealed class RedDragonSaucer : SaucerBase
    {
        public GameObject Player;

        public override void AttackIntruders()
        {
            throw new NotImplementedException();
        }

        void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position,  2f* Time.deltaTime);
        }
    }
}
