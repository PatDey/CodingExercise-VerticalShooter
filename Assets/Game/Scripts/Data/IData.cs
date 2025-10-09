using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CEVerticalShooter
{
    public interface IData<T> where T : Enum
    {
        public T ID { get; }
        public AssetReference Reference { get; }
    }
}
