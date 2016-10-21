using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class AsteroidSpritesRepository : MonoBehaviour {

        private AsteroidTypeItem[] asteroidTypetems;
        public Dictionary<AsteroidTypes, AsteroidTypeItem> asteroidTypeDictionary;

        void Awake()
        {
            asteroidTypetems = GetComponentsInChildren<AsteroidTypeItem>();
            asteroidTypeDictionary = new Dictionary<AsteroidTypes, AsteroidTypeItem>();
            for (int i = 0; i < asteroidTypetems.Length; i++)
            {
                var typeItem = asteroidTypetems[i];
                asteroidTypeDictionary.Add(typeItem.asteroidType, typeItem);
            }
        }
    }
}
