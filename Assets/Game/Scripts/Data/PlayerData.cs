using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public class PlayerData : CharacterData
    {
        [SerializeField]
        private InputActionAsset inputActionAsset;
        [SerializeField]
        private string moveActionName;
        [SerializeField] 
        private string attackActionName;

        public InputAction MoveInputAction => inputActionAsset.FindAction(moveActionName);
        public InputAction AttackInputAction => inputActionAsset.FindAction(attackActionName);
    }
}
