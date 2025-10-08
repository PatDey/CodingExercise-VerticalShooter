using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace CEVerticalShooter.Game.Bullet
{
    [Serializable]
    public class BulletDataCollection
    {
        [SerializeField]
        private List<BulletData> data;

        public BulletData GetBulletDataWithID(BulletID id)
        {
            BulletData bulletData = data.FirstOrDefault(x => x.ID == id);
            Assert.IsNotNull(bulletData, $"Bulletdata with id {id} not found");
            return bulletData;
        }
    }
}
