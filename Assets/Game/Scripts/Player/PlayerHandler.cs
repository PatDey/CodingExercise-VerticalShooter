using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerHandler : ICharacterHandler
    {
        private PlayerData _playerData;
        private BulletDataCollection _bulletDataCollection;
        public float MovementSpeed => _playerData.MovementSpeed;
        public float AttackCooldown => _playerData.AttackCooldown;
        public InputAction MoveInputAction => _playerData.MoveInputAction;
        public InputAction AttackInputAction => _playerData.AttackInputAction;

        public BulletData GetBulletDataWithID(BulletID id) => _bulletDataCollection.GetDataWithID(id);

        public PlayerHandler(PlayerData playerdata, BulletDataCollection bulletDataCollection)
        {
            _playerData = playerdata;
            _bulletDataCollection = bulletDataCollection;
        }
    }
}
