using System;
using UnityEngine;

public enum Stages
{
    RedDust,
    Snow,
    RadioActive
}

public enum Level
{
    LevelOne,
    LevelTwo,
    LevelThree
}

public sealed class CurrentLevel
{
    public Stages stage;
    public Level level;
}

namespace Assets.Scripts
{
    public sealed class GameManager : MonoBehaviour
    {
        public int totalScore;
        public CurrentLevel currentLevel;

        private UiManager uiManager;
        private bool initialized;

        private void FindReferences()
        {
            var referenceManager = GlobalReferenceManager.ReferenceManager;
            var uIManager = referenceManager.uiManager;
            uiManager = uIManager;
        }

        public void Initialize()
        {
            if (initialized)
            {
                return;
            }

            FindReferences();
            currentLevel = new CurrentLevel { level = Level.LevelOne, stage = Stages.RedDust };
            totalScore = 0;
            initialized = true;
        }

        void Update()
        {
            if (!initialized)
            {
                return;
            }
        }

        // Avoiding to string in update
        public void AddScore(object sender, HitEventArgs e)
        {
            var asteroidSize = (AsteroidSize) e.Current;
            var score = PointDamageCalculator.GetAsteroidScore(asteroidSize);
            SetNewScore(score);
        }

        private void SetNewScore(int score)
        {
            uiManager.SetScore(totalScore += score);
        }
    }
}
