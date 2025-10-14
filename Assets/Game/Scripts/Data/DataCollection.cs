using UnityEngine;

namespace CEVerticalShooter.Game.Data
{
    [CreateAssetMenu(fileName = "DataCollection", menuName = "ScriptableObjects/DataCollection", order = 1)]
    public class DataCollection : ScriptableObject
    {
        [SerializeField]
        private PlayerData playerData;
        [SerializeField]
        private EnemyDataCollection enemyDataCollection;
        [SerializeField]
        private BulletDataCollection bulletDataCollection;
        [SerializeField]
        private WinConditionDataCollection winConditionDataCollection;

        public PlayerData PlayerData => playerData;
        public EnemyDataCollection EnemyDataCollection => enemyDataCollection;
        public BulletDataCollection BulletDataCollection => bulletDataCollection;
        public WinConditionDataCollection WinConditionDataCollection => winConditionDataCollection;
    }
}
