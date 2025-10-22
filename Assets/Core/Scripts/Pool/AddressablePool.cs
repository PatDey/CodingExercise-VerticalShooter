using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CEVerticalShooter.Core.Pool
{
    public abstract class AddressablePool<PoolObject> : MonoBehaviour, IPool<PoolObject> where PoolObject : MonoBehaviour
    {
        [SerializeField]
        private int initialElements = 10;

        private AssetReference _poolElement;
        private Stack<PoolObject> _pool = new Stack<PoolObject>();
        private bool _isInitialized = false;
        private GameObject _loadedAsset;
        public virtual void Return(PoolObject poolObjectToReturn)
        {
            if(!_pool.Contains(poolObjectToReturn))
            { 
                poolObjectToReturn.gameObject.SetActive(false);
                poolObjectToReturn.transform.SetParent(transform);
                _pool.Push(poolObjectToReturn);
            }
        }

        public virtual async UniTask<PoolObject> TakeAsync(bool isActive = true, CancellationToken token = default)
        {
            if (!_isInitialized)
                await UniTask.WaitUntil(() => _isInitialized, cancellationToken: token);

            PoolObject nextElement = null;

            if(_pool.Count == 0)
                nextElement = Instantiate(_loadedAsset, transform).GetComponent<PoolObject>();
            else
                nextElement = _pool.Pop();    
                    
            nextElement.gameObject.SetActive(isActive);

            return nextElement;
        }
        public virtual async UniTask InitializeAsync(AssetReference element) 
        {
            if(_isInitialized)
                return;

            _poolElement = element;
            _loadedAsset = await Addressables.LoadAssetAsync<GameObject>(_poolElement);

            for(int i = 0; i < initialElements; i++) 
            {
                GameObject newAsset = Instantiate(_loadedAsset);
                Return(newAsset.GetComponent<PoolObject>());
            }
       
            _isInitialized = true;
        }
    }
}
