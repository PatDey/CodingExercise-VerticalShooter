using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CEVerticalShooter.Core
{
    public abstract class AddressablePool<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour
    {
        [SerializeField]
        private int initialElements = 10;

        private AssetReference _poolElement;
        private Stack<T> _pool = new Stack<T>();
        private bool _isInitialized = false;
        public virtual void Return(T poolObjectToReturn)
        {
            poolObjectToReturn.gameObject.SetActive(false);
            poolObjectToReturn.transform.SetParent(transform);
            _pool.Push(poolObjectToReturn);
        }

        public virtual async UniTask<T> TakeAsync(CancellationToken token)
        {
            if (!_isInitialized)
                await UniTask.WaitUntil(() => _isInitialized, cancellationToken: token);

            T nextElement = null;

            if(_pool.Count == 0)
            { 
                GameObject loadedAsset = await Addressables.LoadAssetAsync<GameObject>(_poolElement);
                nextElement = Instantiate(loadedAsset, transform).GetComponent<T>();
            }
            else
                nextElement = _pool.Pop();     

            nextElement.gameObject.SetActive(true);

            return nextElement;
        }
        public virtual async UniTask InitializeAsync(AssetReference element) 
        {
            if(_isInitialized)
                return;

            _poolElement = element;
            GameObject loadedAsset = await Addressables.LoadAssetAsync<GameObject>(_poolElement);

            for(int i = 0; i < initialElements; i++) 
            {
                GameObject newAsset = Instantiate(loadedAsset);
                Return(newAsset.GetComponent<T>());
            }
       
            _isInitialized = true;
        }
    }
}
