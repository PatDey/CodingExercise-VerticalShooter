using CEVerticalShooter.Core.Pool;
using CEVerticalShooter.Game.Data;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CEVerticalShooter.Game
{
    public abstract class PoolHolder<DataCollection, Data, ID, Pool, PoolObject> : MonoBehaviour where DataCollection : IDataCollection<Data, ID> where Data : IData<ID>, IAssetReference where ID : Enum where Pool : AddressablePool<PoolObject> where PoolObject : MonoBehaviour
    {
        protected DataCollection _dataCollection;
        protected Dictionary<ID, Pool> _poolDictionary = new Dictionary<ID, Pool>();

        private bool _isInitialized;

        public abstract void Construct(DataCollection dataCollection);

        private void Awake()
        {
            InitializePools().Forget(); 
        }
        private async UniTask InitializePools()
        {
            foreach (ID id in Enum.GetValues(typeof(ID)))
            {
                if(_dataCollection.TryToGetDataWithID(id, out Data data))
                { 
                    GameObject poolObject = new GameObject();
                    poolObject.name = $"Pool_{id}";
                    poolObject.transform.parent = transform;
                    Pool pool = poolObject.AddComponent<Pool>();
                    await pool.InitializeAsync(data.Reference);
                    _poolDictionary.Add(id, pool);
                }
            }

            _isInitialized = true;
        }

        public virtual async UniTask<PoolObject> GetPoolObjectWithIDAsync(ID id, bool isActive = true, CancellationToken token = default)
        {
            await UniTask.WaitUntil(() => _isInitialized, cancellationToken: token);

            PoolObject controller = await _poolDictionary[id].TakeAsync(isActive, token);
            return controller;
        }

        public virtual void ReturnPoolObjectWithID(ID id, PoolObject poolObject)
        {
            _poolDictionary[id].Return(poolObject);
        }
    }
}
