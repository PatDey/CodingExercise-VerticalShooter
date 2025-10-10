using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerController : PlaneController
    {
        [SerializeField]
        private Vector2 planeSize;

        private PlayerHandler _playerHandler;
        private float _lastAttackTime = 0f;

        [Inject]
        private void Construct(PlayerHandler playerHandler, PlayArea playArea, BulletPoolHolder bulletPoolHolder)
        {
            _playerHandler = playerHandler;
            Initialize(bulletPoolHolder, playArea, playerHandler.Health);
        }

        private void Update()
        {
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

        public override BulletData GetBulletDataWithID(BulletID id) => _playerHandler.GetBulletDataWithID(id);

        public override void DealDamage(float damage)
        {
            _healthHandler.RemoveHealth(damage);

            if(_healthHandler.IsDead)
            {
                //TODO remove life or end game
            }
        }
    }
}
