using CEVerticalShooter.Game.Bullet;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemyController : PlaneController
    {
        private EnemyHandler _enemyHandler;
        private float _lastAttackTime = 0f;
        private float _flightTime;
        public void Initialize(EnemyHandler enemyHandler, BulletPoolHolder bulletPoolHolder)
        {
            _enemyHandler = enemyHandler;
            _bulletPoolHolder = bulletPoolHolder;
        }

        private void Update()
        {
            _flightTime += Time.deltaTime * _enemyHandler.MovementSpeed;
            Vector3 newPosition = _enemyHandler.GetPosition(_flightTime);
            Vector3 upDirection = (newPosition - transform.position).normalized;
            transform.position = newPosition;
            transform.up = upDirection;

            if(_lastAttackTime + _enemyHandler.AttackCooldown < Time.time)
            { 
                _lastAttackTime = Time.time;
                ShootAsync().Forget();
            }
        }
    }
}
