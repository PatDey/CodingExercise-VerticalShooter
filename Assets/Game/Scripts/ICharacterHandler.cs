using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;

namespace CEVerticalShooter.Game
{
    public interface ICharacterHandler
    {
        public float MovementSpeed { get; }
        public float AttackCooldown { get; }
        public float Health { get; }
        public bool TryToGetBulletDataWithID(BulletID id, out BulletData data);
    }
}
