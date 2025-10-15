using CEVerticalShooter.Game.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CEVerticalShooter.Game.Bullet
{
    public class BulletController : MonoBehaviour
    {
        private BulletPoolHolder _bulletPoolHolder;
        private BulletData _data;
        private IGameService _gameService;
        private PlayArea _playArea;

        private bool _isInitialized;
        public void Initialize(BulletPoolHolder bulletPoolHolder, BulletData data, PlayArea playArea, IGameService gameService)
        {
            _data = data;
            _bulletPoolHolder = bulletPoolHolder;
            _playArea = playArea;
            _gameService = gameService;
            _isInitialized = true;
        }
        private async void Awake()
        {
            await UniTask.WaitUntil(() => _isInitialized);

            _gameService.OnGameOver += GameService_OnGameOver;
        }

        private void OnDestroy()
        {
            if(_gameService != null)
                _gameService.OnGameOver -= GameService_OnGameOver;
        }

        private void Update()
        {
            transform.position += transform.up * Time.deltaTime * _data.Speed;

            if(!_playArea.IsPositionInPlayArea(transform.position))
                ReturnToPool();
        }
        private void ReturnToPool() => _bulletPoolHolder.ReturnPoolObjectWithID(_data.ID, this);

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlaneController planeController = collision.gameObject.GetComponent<PlaneController>();
            if(planeController)
            {
                planeController.DealDamage(_data.Damage);
                ReturnToPool();
            }
        }
        private void GameService_OnGameOver() => ReturnToPool();
    }
}
