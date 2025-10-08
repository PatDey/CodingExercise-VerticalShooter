using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CEVerticalShooter.Core
{
    public interface IPool<T> where T : MonoBehaviour
    {
        public void Return(T poolObject);
        public UniTask<T> TakeAsync(CancellationToken token);
        public UniTask InitializeAsync(AssetReference element);
    }
}
