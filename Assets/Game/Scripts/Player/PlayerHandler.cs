using CEVerticalShooter.Game.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerHandler : ICharacterHandler
    {
        private PlayerData _playerData;
        private InputAction _moveInputAction;
        private InputAction _attackInputAction;

        private float _lastAttackTime = 0f;
        public PlayerHandler(PlayerData playerdata)
        {
            _playerData = playerdata;
            _moveInputAction = playerdata.MoveInputAction;
            _attackInputAction = playerdata.AttackInputAction;
        }

        public Vector2 GetUpDirection() => Vector2.up;

        public Vector2 GetMoveStep()
        {
            return _moveInputAction.ReadValue<Vector2>() * _playerData.MovementSpeed * Time.deltaTime;
        }
        public bool TryAttack()
        {
            if(_lastAttackTime + _playerData.AttackCooldown < Time.time)
            { 
                _lastAttackTime = Time.time;
                return _attackInputAction.IsPressed();
            }

            return false;
        }
    }
}
