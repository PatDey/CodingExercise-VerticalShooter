using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using UnityEngine;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemyHandler : ICharacterHandler
    {
        private EnemyData _enemyData;
        private BulletDataCollection _bulletDataCollection;
        private FlightCurve _curve;
        private Vector2 _curveOffset;

        public EnemyID ID => _enemyData.ID;
        public float MovementSpeed => _enemyData.MovementSpeed;
        public float AttackCooldown => _enemyData.AttackCooldown;
        public BulletData GetBulletDataWithID(BulletID id) => _bulletDataCollection.GetDataWithID(id);
        public Vector3 GetPosition(float time) => (Vector3)_curve.SplineContainer.EvaluatePosition(time) + (Vector3)_curveOffset;
        public EnemyHandler(EnemyData enemyData, BulletDataCollection bulletDataCollection, FlightCurve curve, Vector2 curveOffset)
        {
            _enemyData = enemyData;
            _bulletDataCollection = bulletDataCollection;
            _curve = curve;
            _curveOffset = curveOffset;
        }
    }
}
