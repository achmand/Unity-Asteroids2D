namespace Assets.Scripts
{
    public static class PointDamageCalculator {

        public static int GetAsteroidScore(AsteroidSize asteroidSize)
        {
            switch (asteroidSize)
            {
                    case AsteroidSize.Small:
                    return 100;
                    case AsteroidSize.Medium:
                    return 50;
                case AsteroidSize.Large:
                    return 20;
            }
            return 0;
        }

        public static float GetEnergyLost(AsteroidSize asteroidSize)
        {
            switch (asteroidSize)
            {
                case AsteroidSize.Small:
                    return .2f;
                case AsteroidSize.Medium:
                    return .4f;
                case AsteroidSize.Large:
                    return 1f;
            }
            return 0;
        }
    }
}
