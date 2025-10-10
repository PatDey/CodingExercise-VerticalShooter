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
        [SerializeField]
        protected float health;

        public float MovementSpeed => movementSpeed;
        public float AttackCooldown => attackCooldown;
        public float Health => health;
    }
}
