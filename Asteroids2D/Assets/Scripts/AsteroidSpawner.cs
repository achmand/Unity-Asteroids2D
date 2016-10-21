using UnityEngine;

namespace Assets.Scripts
{
    public sealed class AsteroidSpawner : MonoBehaviour
    {
        public bool debugSpawn;
        private AsteroidSpritesRepository asteroidSpritesRepository;

        void Awake()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            asteroidSpritesRepository = referenceManager.asteroidSpritesRepository;
        }

        private void ActiveAsteroid(AsteroidTypes asteroidType, AsteroidSize asteroidSize, Vector3 startPosition)
        {
            var asteroidTypeItem = asteroidSpritesRepository.asteroidTypeDictionary[asteroidType];
            var asteroidTypeSizeItem = asteroidTypeItem.asteroidsTypeSizeItems[asteroidSize];
            var asteroidSprite = asteroidTypeSizeItem.asteroidSprite;

            var asteroid = AsteroidsPooler.currentInstance.GetAsteroid(asteroidSize);
            var asteroidTransform = asteroid.gameObject.transform;
            asteroidTransform.localPosition = startPosition;
            var baseAsteroid = asteroid.GetComponent<BaseAsteroid>();
            asteroid.SetActive(true);
            baseAsteroid.asteroidType = asteroidType;
            baseAsteroid._spriteRenderer.sprite = asteroidSprite;
        }

        public void GenerateAsteroid(AsteroidSize asteroidSize)
        {
            Vector3 enemyPosition;
            do
            {
                var randomY = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
                var randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
                enemyPosition = new Vector3(randomX, randomY);
            } while (Vector3.Distance(transform.position, enemyPosition) < transform.localScale.magnitude);

            var asteroidType = (AsteroidTypes)Random.Range(1, 4);
            ActiveAsteroid(asteroidType, asteroidSize, enemyPosition);
        }

        public void HandleChildAsteroid(AsteroidTypes asteroidType,AsteroidSize asteroidSize, Vector3 startPosition)
        {
            var childAsteroid = asteroidSize == AsteroidSize.Large ? AsteroidSize.Medium : AsteroidSize.Small;
            GenerateChildAsteroid(asteroidType, childAsteroid,startPosition);
            GenerateChildAsteroid(asteroidType, childAsteroid, startPosition);
        }

        private void GenerateChildAsteroid(AsteroidTypes asteroidType , AsteroidSize asteroidSize, Vector3 startPosition)
        {
            var childAsteroid = asteroidSize == AsteroidSize.Large ? AsteroidSize.Medium : AsteroidSize.Small;
            var asteroid = AsteroidsPooler.currentInstance.GetAsteroid(childAsteroid);
            if (asteroid == null)
            {
                return;
            }

            ActiveAsteroid(asteroidType, asteroidSize, startPosition);
        }

        void Update()
        {
            if (debugSpawn)
            {
                GenerateAsteroid(AsteroidSize.Large);
                debugSpawn = false;
            }
        }
    }
}
