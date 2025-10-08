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
        private string attackActionName;
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float attackCooldown;

        public InputAction MoveInputAction => inputActionAsset.FindAction(moveActionName);
        public InputAction AttackInputAction => inputActionAsset.FindAction(attackActionName);
        public float MovementSpeed => movementSpeed;
        public float AttackCooldown => attackCooldown;
    }
}
