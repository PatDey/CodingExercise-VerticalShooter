using UnityEngine;

namespace CEVerticalShooter.Game
{
    public interface ICharacterHandler
    {
        public float MovementSpeed { get; }
        public float AttackCooldown { get; }
    }
}
