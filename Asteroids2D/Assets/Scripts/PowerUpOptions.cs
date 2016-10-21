using UnityEngine;

namespace Assets.Scripts
{
    public enum PowerUps
    {
        HellFire,
        SonarField,
        Energy,
        Health
    }

    public enum OccurrenceTypes
    {
        Common,
        Uncommon,
        Rare,
        Lucky
    }

    public sealed class PowerUpOptions : MonoBehaviour
    {
        public float lifeTime;
        public OccurrenceTypes occurrenceType;
        public PowerUps powerUpType;
    }
}
