using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Health;
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
        protected PlayArea _playArea;
        protected HealthHandler _healthHandler;
        protected IGameService _gameService;
        protected bool _isInitialized;
        public void Initialize(IGameService gameService, BulletPoolHolder bulletPoolHolder, PlayArea playArea, float health)
        {
            _gameService = gameService;
            _bulletPoolHolder = bulletPoolHolder;
            _playArea = playArea;
            _healthHandler = new HealthHandler(health);
            _isInitialized = true;
        }

        protected async UniTask ShootAsync()
        {
            if (TryToGetBulletDataWithID(bulletID, out BulletData data))
            {
                BulletController bulletController = await _bulletPoolHolder.GetPoolObjectWithIDAsync(bulletID, _tokenSource.Token);
                bulletController.Initialize(_bulletPoolHolder, data, _playArea, _gameService);
                bulletController.transform.position = shootTransform.position;
                bulletController.transform.up = shootTransform.up;
                bulletController.gameObject.layer = gameObject.layer;
            }
        }
        public abstract bool TryToGetBulletDataWithID(BulletID id, out BulletData data);
        public abstract void DealDamage(float damage);

        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
