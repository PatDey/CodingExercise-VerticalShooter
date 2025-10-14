using CEVerticalShooter.Game.Bullet;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class BulletData : IData<BulletID>, IAssetReference
    {
        [SerializeField]
        private BulletID id;
        [SerializeField]
        private AssetReference reference;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float damage;

        public BulletID ID => id;
        public AssetReference Reference => reference;
        public float Speed => speed;
        public float Damage => damage;
    }
}
