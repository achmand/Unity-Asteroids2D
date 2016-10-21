using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class ObjectPooler : MonoBehaviour
    {
        public int pooledAmount;
        protected bool initialized;

        public abstract void Initialize();

        public void PopulateList(List<GameObject> pooledList, GameObject pooledObject, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var obj = Instantiate(pooledObject);
                obj.SetActive(false);
                pooledList.Add(obj);
            }
        }
    }

    public sealed class AsteroidsPooler : ObjectPooler
    {
        public static AsteroidsPooler currentInstance;
        public GameObject asteroidsSmall;
        public GameObject asteroidsMedium;
        public GameObject asteroidsLarge;

        [HideInInspector] private List<GameObject> pooledAsteroidsSmall;
        [HideInInspector] private List<GameObject> pooledAsteroidsMedium;
        [HideInInspector] private List<GameObject> pooledAsteroidsLarge;

        public override void Initialize()
        {
            if (initialized)
            {
                return;
            }

            currentInstance = this;
            initialized = true;
            pooledAsteroidsSmall = new List<GameObject>();
            PopulateList(pooledAsteroidsSmall, asteroidsSmall, pooledAmount*4);
            pooledAsteroidsMedium = new List<GameObject>();
            PopulateList(pooledAsteroidsMedium, asteroidsMedium, pooledAmount*2);
            pooledAsteroidsLarge = new List<GameObject>();
            PopulateList(pooledAsteroidsLarge, asteroidsLarge, pooledAmount);
        }

        public GameObject GetAsteroid(AsteroidSize asteroidSize)
        {
            switch (asteroidSize)
            {
                case AsteroidSize.Small:
                    return pooledAsteroidsSmall.FirstOrDefault(a => !a.gameObject.activeInHierarchy);
                case AsteroidSize.Medium:
                    return pooledAsteroidsMedium.FirstOrDefault(a => !a.gameObject.activeInHierarchy);
                case AsteroidSize.Large:
                    return pooledAsteroidsLarge.FirstOrDefault(a => !a.gameObject.activeInHierarchy);
            }
            return null;
        }
    }
}