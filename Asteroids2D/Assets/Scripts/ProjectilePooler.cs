using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class ProjectilePooler : MonoBehaviour {

        public static ProjectilePooler currentInstance;
        public GameObject pooledObject;
        public ParticleSystem explosionParticle;
        public int pooledAmount;
        public bool willGrow;

        List<GameObject> pooledObjectRight;
        List<GameObject> pooledObjectLeft;
        List<ParticleSystem> pooledExplosions;

        void Awake()
        {
            currentInstance = this;
        }

        void Start () {
            pooledObjectRight = new List<GameObject>();
            for (int i = 0; i < pooledAmount; i++) {
                var obj = Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjectRight.Add(obj);
            }
            pooledObjectLeft = new List<GameObject>();
            for (int i = 0; i < pooledAmount; i++)
            {
                var obj = Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjectLeft.Add(obj);
            }
            pooledExplosions = new List<ParticleSystem>();
            for (int i = 0; i < 30; i++)
            {
                var obj = Instantiate(explosionParticle);
                obj.gameObject.SetActive(false);
                pooledExplosions.Add(obj);
            }
        }
	
        public GameObject GetPooledObjectLeft() {
            for (int i = 0; i < pooledObjectLeft.Count; i++)
            {
                var tmpBullet = pooledObjectLeft[i];
                if (!tmpBullet.activeInHierarchy)
                {
                    return tmpBullet;
                }
                if (!willGrow)
                {
                    continue;
                }
                var obj = Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjectLeft.Add(obj);
                return obj;
            }
            return null;
        }

        public GameObject GetPooledObjectRight()
        {
            for (int i = 0; i < pooledObjectRight.Count; i++)
            {
                var tmpBullet = pooledObjectRight[i];
                if (!tmpBullet.activeInHierarchy)
                {
                    return tmpBullet;
                }
                if (!willGrow)
                {
                    continue;
                }
                var obj = Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjectRight.Add(obj);
                return obj;
            }
            return null;
        }

        public ParticleSystem GetExplosionParticleSystem()
        {
            for (int i = 0; i < pooledExplosions.Count; i++)
            {
                var tmpBullet = pooledExplosions[i];
                if (!tmpBullet.gameObject.activeInHierarchy)
                {
                    return tmpBullet;
                }
                if (!willGrow)
                {
                    continue;
                }
                var obj = Instantiate(explosionParticle);
                obj.gameObject.SetActive(false);
                pooledExplosions.Add(obj);
                return obj;
            }
            return null;
        }
    }
}
