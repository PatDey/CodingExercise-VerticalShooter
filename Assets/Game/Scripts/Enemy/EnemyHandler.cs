using CEVerticalShooter.Game.Data;
using UnityEngine;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemyHandler : ICharacterHandler
    {
        private EnemyData _enemyData;
        private FlightCurve _curve;
        private Vector2 _curveOffset;

        private float _lastAttackTime = 0f;
        private float _flightTime = 0f;
        private Vector2 _upDirection;

        private Vector3 GetPosition(float time) => (Vector3)_curve.SplineContainer.EvaluatePosition(time) + (Vector3)_curveOffset;
        public EnemyHandler(EnemyData enemyData, FlightCurve curve, Vector2 curveOffset)
        {
            _enemyData = enemyData;
            _curve = curve;
            _curveOffset = curveOffset;
            _lastAttackTime = Time.time;
        }
        public Vector2 GetMoveStep()
        {   
            Vector3 oldPosition = GetPosition(_flightTime);
            _flightTime += Time.deltaTime * _enemyData.MovementSpeed;
            Vector3 newPosition = GetPosition(_flightTime);
            Vector3 moveStep = newPosition - oldPosition;
            _upDirection = moveStep.normalized;
            return moveStep;
        }
        public bool TryAttack()
        {
            if(_lastAttackTime + _enemyData.AttackCooldown < Time.time)
            { 
                _lastAttackTime = Time.time;
                return true;
            }

            return false;
        }

        public Vector2 GetUpDirection() => _upDirection;
    }
}
