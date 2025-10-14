using CEVerticalShooter.Game.Enemy;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class EnemyData : CharacterData, IData<EnemyID>, IAssetReference
    {
        [SerializeField]
        private EnemyID id;

        [SerializeField]
        private AssetReference reference;

        [SerializeField]
        private int score;

        [SerializeField]
        private int minGroupSize;

        [SerializeField]
        private int maxGroupSize;

        [SerializeField]
        private float spawnIntervall;

        [SerializeField] 
        private float collisionDamage;
        public EnemyID ID => id;
        public AssetReference Reference => reference;
        public int Score => score;
        public int MinGroupSize => minGroupSize;
        public int MaxGroupSize => maxGroupSize;
        public float SpawnIntervall => spawnIntervall;
        public float CollisionDamage => collisionDamage;

    }
}
