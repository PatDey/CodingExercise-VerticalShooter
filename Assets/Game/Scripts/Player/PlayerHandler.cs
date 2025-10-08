using UnityEngine;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerHandler : ICharacterHandler
    {
        private PlayerSettings _playerSettings;
        private InputAction _moveInputAction;
        public PlayerHandler(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
            _moveInputAction = playerSettings.MoveInputAction;
        }
        public Vector2 Move()
        {
            return _moveInputAction.ReadValue<Vector2>() * _playerSettings.MovementSpeed * Time.deltaTime;
        }
    }
}
