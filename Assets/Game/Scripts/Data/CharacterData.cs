using System;
using UnityEngine;

namespace CEVerticalShooter.Game.Data
{
    [Serializable]
    public abstract class CharacterData
    {
        [SerializeField]
        protected float movementSpeed;
        [SerializeField]
        protected float attackCooldown;

        public float MovementSpeed => movementSpeed;
        public float AttackCooldown => attackCooldown;
    }
}
