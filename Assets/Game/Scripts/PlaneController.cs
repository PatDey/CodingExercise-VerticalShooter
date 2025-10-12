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
            BulletController bulletController = await _bulletPoolHolder.GetPoolObjectWithIDAsync(bulletID, _tokenSource.Token);
            BulletData bulletData = GetBulletDataWithID(bulletID);
            bulletController.Initialize(_bulletPoolHolder, bulletData, _playArea, _gameService);
            bulletController.transform.position = shootTransform.position;
            bulletController.transform.up = shootTransform.up;
            bulletController.gameObject.layer = gameObject.layer;
        }
        public abstract BulletData GetBulletDataWithID(BulletID id);
        public abstract void DealDamage(float damage);

        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
