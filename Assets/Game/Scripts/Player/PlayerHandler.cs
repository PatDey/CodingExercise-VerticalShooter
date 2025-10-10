using CEVerticalShooter.Game.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerHandler : ICharacterHandler
    {
        private PlayerData _playerData;
        public float MovementSpeed => _playerData.MovementSpeed;
        public float AttackCooldown => _playerData.AttackCooldown;
        public InputAction MoveInputAction => _playerData.MoveInputAction;
        public InputAction AttackInputAction => _playerData.AttackInputAction;

        public PlayerHandler(PlayerData playerdata)
        {
            _playerData = playerdata;
        }
    }
}
