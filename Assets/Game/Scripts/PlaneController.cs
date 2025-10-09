using CEVerticalShooter.Game.Bullet;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using VContainer;

namespace CEVerticalShooter.Game
{
    public class PlaneController : MonoBehaviour, IDisposable
    {
        [Header("Components")]
        [SerializeField]
        private Transform shootTransform;

        [Header("Settings")]
        [SerializeField]
        private BulletID bulletID;

        private readonly CancellationTokenSource _tokenSource = new();
        private ICharacterHandler _characterHandler;
        private BulletPoolHolder _bulletPoolHolder;

        [Inject]
        private void Construct(ICharacterHandler characterHandler, BulletPoolHolder bulletPoolHolder)
        {
            _characterHandler = characterHandler;
            _bulletPoolHolder = bulletPoolHolder;
        }

        public void Initialize(ICharacterHandler characterHandler, BulletPoolHolder bulletPoolHolder)
        {
            _characterHandler = characterHandler;
            _bulletPoolHolder = bulletPoolHolder;
        }

        private void Update()
        {
            transform.position += (Vector3)_characterHandler.GetMoveStep();
            transform.up = _characterHandler.GetUpDirection();

            if(_characterHandler.TryAttack())
            {
                ShootAsync().Forget();
            }
        }

        private async UniTask ShootAsync()
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
