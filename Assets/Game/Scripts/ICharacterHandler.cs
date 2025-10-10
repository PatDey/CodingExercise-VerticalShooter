using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using UnityEngine;

namespace CEVerticalShooter.Game
{
    public interface ICharacterHandler
    {
        public float MovementSpeed { get; }
        public float AttackCooldown { get; }
        public float Health { get; }
        public BulletData GetBulletDataWithID(BulletID id);
    }
}
