using UnityEngine;

namespace CEVerticalShooter.Game
{
    public class PlayArea : MonoBehaviour
    {
        [SerializeField]
        private Vector2 areaSize;

        public bool IsPositionInPlayArea(Vector2 position) => IsXInPlayArea(position.x) && IsYInPlayArea(position.y);

        public bool IsXInPlayArea(float Xposition)
        {
            float halfAreaX = areaSize.x * 0.5f;
            return (Xposition < halfAreaX && Xposition > -halfAreaX);
        }

        public bool IsYInPlayArea(float Yposition)
        {
            float halfAreaY = areaSize.y * 0.5f;
            return (Yposition < halfAreaY && Yposition > -halfAreaY);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Vector3.zero, areaSize);
        }
    }
}
