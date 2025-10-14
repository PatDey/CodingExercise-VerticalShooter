using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerController : PlaneController
    {
        [Header("Components")]
        [SerializeField]
        private HealthBar healthBar;

        [Header("Settings")]
        [SerializeField]
        private Vector2 planeSize;

        private PlayerHandler _playerHandler;
        private float _lastAttackTime = 0f;

        private Vector2 _startPosition;

        [Inject]
        private void Construct(PlayerHandler playerHandler, PlayArea playArea, BulletPoolHolder bulletPoolHolder, IGameService gameService)
        {
            _playerHandler = playerHandler;
            Initialize(gameService, bulletPoolHolder, playArea, playerHandler.Health);
            healthBar.Initialize(_healthHandler);
        }

        private void Awake()
        {
            _startPosition = transform.position;
            _gameService.OnNewGame += GameService_OnNewGame;
            _gameService.OnGameOver += GameService_OnGameOver;
        }

        private void OnDestroy()
        {
            if(_gameService != null)
            { 
                _gameService.OnNewGame -= GameService_OnNewGame;
                _gameService.OnGameOver -= GameService_OnGameOver;
            }
        }

        private void Update()
        {
            if (!_gameService.IsRunning)
            {
                return;
            }

            Vector3 newPosition = transform.position + ((Vector3)_playerHandler.MoveInputAction.ReadValue<Vector2>() * _playerHandler.MovementSpeed * Time.deltaTime);

            float halfSizeX = planeSize.x * 0.5f;
            float halfSizeY = planeSize.y * 0.5f;
            Vector2 minSize = new Vector2(newPosition.x - halfSizeX, newPosition.y - halfSizeY);
            Vector2 maxSize = new Vector2(newPosition.x + halfSizeX, newPosition.y + halfSizeY);

            Vector2 finalPosition = transform.position;

            if(_playArea.IsXInPlayArea(minSize.x) && _playArea.IsXInPlayArea(maxSize.x))
                finalPosition.x = newPosition.x;

            if(_playArea.IsYInPlayArea(minSize.y) && _playArea.IsYInPlayArea(maxSize.y))
                finalPosition.y = newPosition.y;

            transform.position = finalPosition;

            if(_playerHandler.AttackInputAction.IsPressed() && _lastAttackTime + _playerHandler.AttackCooldown < Time.time)
            { 
                _lastAttackTime = Time.time;
                ShootAsync().Forget();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(Vector3.zero, planeSize);
        }

        public override bool TryToGetBulletDataWithID(BulletID id, out BulletData data) => _playerHandler.TryToGetBulletDataWithID(id, out data);

        public override void DealDamage(float damage)
        {
            _healthHandler.RemoveHealth(damage);

            if(_healthHandler.IsDead)
            {
                _gameService.ReduceLife();
                if(_gameService.Lives > 0)
                {
                    _healthHandler.ResetHealth();
                    //add Invincible frame
                }
            }
        }

        private void GameService_OnNewGame()
        {
            _healthHandler.ResetHealth();
            transform.position = _startPosition;
            gameObject.SetActive(true);
        }
        private void GameService_OnGameOver()
        {
            gameObject.SetActive(false);
        }
    }
}
