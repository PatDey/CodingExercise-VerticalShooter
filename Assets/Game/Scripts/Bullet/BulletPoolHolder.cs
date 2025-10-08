using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;

namespace CEVerticalShooter.Game.Bullet
{
    public class BulletPoolHolder : MonoBehaviour
    {
        private BulletDataCollection _bulletDataCollection;
        private Dictionary<BulletID, BulletPool> _bulletPoolDictionary = new Dictionary<BulletID, BulletPool>();

        [Inject]
        private void Construct(DataCollection dataCollection)
        {
            _bulletDataCollection = dataCollection.BulletDataCollection;
            InitializePools().Forget();
        }
        private async UniTask InitializePools()
        {
            foreach (BulletID id in Enum.GetValues(typeof(BulletID)))
            {
                BulletData bulletData = _bulletDataCollection.GetBulletDataWithID(id);
                GameObject poolObject = new GameObject();
                poolObject.name = $"BulletPool_{id}";
                poolObject.transform.parent = transform;
                BulletPool bulletPool = poolObject.AddComponent<BulletPool>();
                await bulletPool.InitializeAsync(bulletData.Reference);
                _bulletPoolDictionary.Add(id, bulletPool);
            }
        }

        public async UniTask<BulletController> GetBulletWithIDAsync(BulletID id, CancellationToken token)
        {
            BulletData bulletData = _bulletDataCollection.GetBulletDataWithID(id);
            BulletController bulletController = await _bulletPoolDictionary[id].TakeAsync(token);
            bulletController.Initialize(bulletData);
            return bulletController;
        }

        public void ReturnBulletWithID(BulletID id, BulletController bullet)
        {
            _bulletPoolDictionary[id].Return(bullet);
        }
    }
}
