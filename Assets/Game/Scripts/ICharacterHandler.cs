using UnityEngine;

namespace CEVerticalShooter.Game
{
    public interface ICharacterHandler
    {
        public Vector2 GetMoveStep();
        public Vector2 GetUpDirection();
        public bool TryAttack();
    }
}
