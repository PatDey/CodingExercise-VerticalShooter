using UnityEngine;

namespace CEVerticalShooter.Game
{
    public interface ICharacterHandler
    {
        public Vector2 Move();
        public bool Attack();
    }
}
