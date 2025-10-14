using CEVerticalShooter.Game.Enemy;
using System;
using UnityEngine;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class EnemyDataCollection : IDDataCollection<EnemyData, EnemyID>
    {
        [SerializeField]
        private float timeBetweenSpawns;
        public float TimeBetweenSpawns => timeBetweenSpawns;
    }
}
