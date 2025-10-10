using CEVerticalShooter.Game.Bullet;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerController : PlaneController
    {
        private PlayerHandler _playerHandler;
        private float _lastAttackTime = 0f;

        [Inject]
        private void Construct(PlayerHandler playerHandler, BulletPoolHolder bulletPoolHolder)
        {
            _playerHandler = playerHandler;
            _bulletPoolHolder = bulletPoolHolder;
        }

        private void Update()
        {
            transform.position += (Vector3)_playerHandler.MoveInputAction.ReadValue<Vector2>() * _playerHandler.MovementSpeed * Time.deltaTime;

            if(_playerHandler.AttackInputAction.IsPressed() && _lastAttackTime + _playerHandler.AttackCooldown < Time.time)
            { 
                _lastAttackTime = Time.time;
                ShootAsync().Forget();
            }
        }
    }
}
