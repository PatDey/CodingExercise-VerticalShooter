using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerHandler : ICharacterHandler
    {
        private PlayerData _playerData;
        public BulletID BulletID => _playerData.BulletID;
        public float MovementSpeed => _playerData.MovementSpeed;
        public float AttackCooldown => _playerData.AttackCooldown;
        public float Health => _playerData.Health;
        public int Lifes => Lifes;
        public InputAction MoveInputAction => _playerData.MoveInputAction;
        public InputAction AttackInputAction => _playerData.AttackInputAction;

        public PlayerHandler(PlayerData playerdata)
        {
            _playerData = playerdata;
        }
    }
}
