using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    public class PlayerHandler : ICharacterHandler
    {
        private PlayerData _playerData;
        private BulletDataCollection _bulletDataCollection;
        public float MovementSpeed => _playerData.MovementSpeed;
        public float AttackCooldown => _playerData.AttackCooldown;
        public float Health => _playerData.Health;
        public int Lifes => Lifes;
        public InputAction MoveInputAction => _playerData.MoveInputAction;
        public InputAction AttackInputAction => _playerData.AttackInputAction;

        public bool TryToGetBulletDataWithID(BulletID id, out BulletData data) => _bulletDataCollection.TryToGetDataWithID(id, out data);

        public PlayerHandler(PlayerData playerdata, BulletDataCollection bulletDataCollection)
        {
            _playerData = playerdata;
            _bulletDataCollection = bulletDataCollection;
        }
    }
}
