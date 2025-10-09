using CEVerticalShooter.Game.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class EnemyDataCollection : IDataCollection<EnemyData, EnemyID>
    {
        [SerializeField]
        private List<EnemyData> data;

        public EnemyData GetDataWithID(EnemyID id)
        {
            EnemyData enemyData = data.FirstOrDefault(x => x.ID == id);
            Assert.IsNotNull(enemyData, $"Data with id {id} not found");
            return enemyData;
        }
    }
}
