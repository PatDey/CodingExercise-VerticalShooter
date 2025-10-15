using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using UnityEngine;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemyHandler : ICharacterHandler
    {
        private EnemyData _enemyData;
        private FlightCurve _curve;
        private Vector2 _curveOffset;

        public EnemyID ID => _enemyData.ID;
        public BulletID BulletID => _enemyData.BulletID;
        public float MovementSpeed => _enemyData.MovementSpeed;
        public float AttackCooldown => _enemyData.AttackCooldown;
        public float CollisionDamage => _enemyData.CollisionDamage;
        public float Health => _enemyData.Health;
        public int Score => _enemyData.Score;
        public Vector3 GetPosition(float time) => (Vector3)_curve.SplineContainer.EvaluatePosition(time) + (Vector3)_curveOffset;

        public EnemyHandler(EnemyData enemyData, FlightCurve curve, Vector2 curveOffset)
        {
            _enemyData = enemyData;
            _curve = curve;
            _curveOffset = curveOffset;
        }
    }
}
