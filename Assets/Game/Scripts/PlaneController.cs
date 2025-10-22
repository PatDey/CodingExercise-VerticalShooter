using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Health;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace CEVerticalShooter.Game
{
    public abstract class PlaneController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        protected Transform shootTransform;

        protected readonly CancellationTokenSource _tokenSource = new();
        protected BulletPoolHolder _bulletPoolHolder;
        protected BulletDataCollection _bulletDataCollection;
        protected PlayArea _playArea;
        protected HealthHandler _healthHandler;
        protected IGameService _gameService;
        protected bool _isInitialized;
        protected BulletID _bulletID;
        public void Initialize(IGameService gameService, BulletPoolHolder bulletPoolHolder, BulletDataCollection bulletDataCollection, 
                                PlayArea playArea, float health, BulletID bulletID)
        {
            _gameService = gameService;
            _bulletPoolHolder = bulletPoolHolder;
            _bulletDataCollection = bulletDataCollection;
            _playArea = playArea;
            _bulletID = bulletID;
            _healthHandler = new HealthHandler(health);
            _isInitialized = true;
        }

        public virtual void OnDestroy()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        protected async UniTask ShootAsync()
        {
            if (_bulletDataCollection.TryToGetDataWithID(_bulletID, out BulletData data))
            {
                BulletController bulletController = await _bulletPoolHolder.GetPoolObjectWithIDAsync(_bulletID, token: _tokenSource.Token);
                bulletController.Initialize(_bulletPoolHolder, data, _playArea, _gameService);
                bulletController.transform.position = shootTransform.position;
                bulletController.transform.up = shootTransform.up;
                bulletController.gameObject.layer = gameObject.layer;
            }
        }
        public abstract void DealDamage(float damage);
    }
}
