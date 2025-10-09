using CEVerticalShooter.Game.Bullet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class BulletDataCollection : IDataCollection<BulletData, BulletID>
    {
        [SerializeField]
        private List<BulletData> data;

        public BulletData GetDataWithID(BulletID id)
        {
            BulletData bulletData = data.FirstOrDefault(x => x.ID == id);
            Assert.IsNotNull(bulletData, $"Data with id {id} not found");
            return bulletData;
        }
    }
}
