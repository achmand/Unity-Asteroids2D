using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public enum AsteroidTypes
    {
        None = 0,
        Gold = 1,
        Fire = 2,
        Dust = 3,
        Stone = 4 
    }

    public sealed class AsteroidTypeItem : MonoBehaviour
    {
        public AsteroidTypes asteroidType;
        [HideInInspector] public Dictionary<AsteroidSize,AsteroidTypeSizeItem> asteroidsTypeSizeItems;

        void Awake()
        {
            asteroidsTypeSizeItems = GetComponentsInChildren<AsteroidTypeSizeItem>().ToDictionary(a => a.asteroidSize, a => a);
        }
    }
}
