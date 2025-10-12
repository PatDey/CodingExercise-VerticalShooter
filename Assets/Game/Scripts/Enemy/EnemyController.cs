using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Player;
using CEVerticalShooter.Game.Score;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemyController : PlaneController
    {
        private EnemyPoolHolder _enemyPoolHolder;
        private EnemyHandler _enemyHandler;
        private IScoreService _scoreService;
        private float _lastAttackTime = 0f;
        private float _flightTime = 0f;

        public void Initialize(EnemyHandler enemyHandler, PlayArea playArea, BulletPoolHolder bulletPoolHolder, EnemyPoolHolder enemyPoolHolder, IScoreService scoreService, IGameService gameService)
        {
            _enemyPoolHolder = enemyPoolHolder;
            _enemyHandler = enemyHandler;
            _scoreService = scoreService;
            Initialize(gameService, bulletPoolHolder, playArea, _enemyHandler.Health);

            _lastAttackTime = 0f;
            _flightTime = 0f;
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
            _flightTime += Time.deltaTime * _enemyHandler.MovementSpeed;
            Vector3 newPosition = _enemyHandler.GetPosition(_flightTime);
            Vector3 upDirection = (newPosition - transform.position).normalized;
            transform.position = newPosition;
            transform.up = upDirection;

            if(_playArea.IsPositionInPlayArea(transform.position) && _lastAttackTime + _enemyHandler.AttackCooldown < Time.time)
            { 
                _lastAttackTime = Time.time;
                ShootAsync().Forget();
            }

            if(_flightTime >= 1)
                ReturnToPool();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if(playerController)
            {
                playerController.DealDamage(_enemyHandler.CollisionDamage);
                ReturnToPool();
            }
        }

        private void ReturnToPool() => _enemyPoolHolder.ReturnPoolObjectWithID(_enemyHandler.ID, this);

        public override BulletData GetBulletDataWithID(BulletID id) => _enemyHandler.GetBulletDataWithID(id);

        public override void DealDamage(float damage)
        {
            _healthHandler.RemoveHealth(damage);

            if(_healthHandler.IsDead)
            { 
                _scoreService.AddScore(_enemyHandler.Score);
                ReturnToPool();
            }
        }

        private void GameService_OnGameOver() => ReturnToPool();
    }
}
