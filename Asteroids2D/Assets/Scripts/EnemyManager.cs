using UnityEngine;

namespace Assets.Scripts
{
    public sealed class EnemyManager : MonoBehaviour
    {
        private AsteroidSpawner asteroidSpawner;
        private float asteroidTimer;
        private bool initialized;
        private CurrentLevel currentLevel;

        private readonly AsteroidSize[] levelOneFrequency =
        {
            AsteroidSize.Small,
            AsteroidSize.Small,
            AsteroidSize.Small,
            AsteroidSize.Medium,
            AsteroidSize.Medium,
            AsteroidSize.Large
        };

        private readonly AsteroidSize[] levelTwoFrequency =
        {
            AsteroidSize.Medium,
            AsteroidSize.Medium,
            AsteroidSize.Small,
            AsteroidSize.Small,
            AsteroidSize.Large,
            AsteroidSize.Large
        };

        private readonly AsteroidSize[] levelThreeFrequency =
        {
            AsteroidSize.Large,
            AsteroidSize.Large,
            AsteroidSize.Large,
            AsteroidSize.Medium,
            AsteroidSize.Medium,
            AsteroidSize.Small
        };

        // TODO : Event needed to change stage timer
        private void FindReferences()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            asteroidSpawner = referenceManager.asteroidSpawner;
            currentLevel = referenceManager.gameManager.currentLevel;
            var stage = currentLevel.stage;
            asteroidTimer = StageTimer(stage);
        }

        public void Initialize()
        {
            if (initialized)
            {
                return;
            }
            FindReferences();
            initialized = true;
        }

        void Update()
        {
            if (initialized)
            {
                HandleAsteroids(currentLevel);
            }
        }

        private void HandleAsteroids(CurrentLevel currentLevel)
        {
            var level = currentLevel.level;
            if (asteroidTimer < Time.time)
            {
                var asteroidSize = CalcualteAsteroidSize(level);
                asteroidSpawner.GenerateAsteroid(asteroidSize);
                asteroidTimer = StageTimer(Stages.RedDust);
            }
        }

        private AsteroidSize[] GetLevelFrequency(Level level)
        {
            switch (level)
            {
                case Level.LevelOne:
                    return levelOneFrequency;
                case Level.LevelTwo:
                    return levelTwoFrequency;
                case Level.LevelThree:
                    return levelThreeFrequency;
            }
            return null;
        }

        private AsteroidSize CalcualteAsteroidSize(Level level)
        {
            var levelFrequency = GetLevelFrequency(level);
            var randomPicker = Random.Range(0, levelFrequency.Length);
            var asteroidSize = levelFrequency[randomPicker];
            return asteroidSize;
        }

        private float StageTimer(Stages stage)
        {
            var time = Time.time;
            switch (stage)
            {
                case Stages.RedDust:
                    return time + 1.5f;
                case Stages.Snow:
                    return time + .9f;
                case Stages.RadioActive:
                    return time + .1f;
            }
            return 0;
        }
    }
}