using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace CEVerticalShooter.Game
{
    public abstract class PlaneController : MonoBehaviour, IDisposable
    {
        [Header("Components")]
        [SerializeField]
        protected Transform shootTransform;

        [Header("Settings")]
        [SerializeField]
        protected BulletID bulletID;

        protected readonly CancellationTokenSource _tokenSource = new();
        protected BulletPoolHolder _bulletPoolHolder;

        protected async UniTask ShootAsync()
        {
            BulletController bulletController = await _bulletPoolHolder.GetPoolObjectWithIDAsync(bulletID, _tokenSource.Token);
            bulletController.transform.position = shootTransform.position;
            bulletController.transform.up = shootTransform.up;
        }

        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
