using UnityEngine;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField]
        private InputActionAsset inputActionAsset;
        [SerializeField]
        private string moveActionName;
        [SerializeField]
        private float movementSpeed;

        public InputAction MoveInputAction => inputActionAsset.FindAction(moveActionName);
        public float MovementSpeed => movementSpeed;
    }
}
