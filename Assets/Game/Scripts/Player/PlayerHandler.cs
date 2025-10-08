using UnityEngine;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerHandler : ICharacterHandler
    {
        private PlayerSettings _playerSettings;
        private InputAction _moveInputAction;
        private InputAction _attackInputAction;

        private float _lastAttackTime = 0f;
        public PlayerHandler(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
            _moveInputAction = playerSettings.MoveInputAction;
            _attackInputAction = playerSettings.AttackInputAction;
        }
        public Vector2 Move()
        {
            return _moveInputAction.ReadValue<Vector2>() * _playerSettings.MovementSpeed * Time.deltaTime;
        }
        public bool Attack()
        {
            if(_lastAttackTime + _playerSettings.AttackCooldown < Time.time)
            { 
                _lastAttackTime = Time.time;
                return _attackInputAction.IsPressed();
            }

            return false;
        }
    }
}
