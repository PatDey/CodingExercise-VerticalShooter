using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemyController : PlaneController
    {
        private EnemyPoolHolder _enemyPoolHolder;
        private EnemyHandler _enemyHandler;
        private float _lastAttackTime = 0f;
        private float _flightTime = 0f;

        public void Initialize(EnemyHandler enemyHandler, PlayArea playArea, BulletPoolHolder bulletPoolHolder, EnemyPoolHolder enemyPoolHolder)
        {
            _enemyPoolHolder = enemyPoolHolder;
            _enemyHandler = enemyHandler;
            _bulletPoolHolder = bulletPoolHolder;
            _playArea = playArea;
            _lastAttackTime = 0f;
            _flightTime = 0f;
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
            { 
                _enemyPoolHolder.ReturnPoolObjectWithID(_enemyHandler.ID, this);
            }
        }

        public override BulletData GetBulletDataWithID(BulletID id) => _enemyHandler.GetBulletDataWithID(id);
    }
}
