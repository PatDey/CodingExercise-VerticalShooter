using System;
using UnityEngine;

namespace CEVerticalShooter.Game.Health
{
    public class HealthHandler
    {
        private float _totalHealth;
        private float _currentHealth;
        public float TotalHealth => _totalHealth;
        public float CurrentHealth => _currentHealth;
        public bool IsDead => CurrentHealth == 0;

        public Action<float, float> onHealthChange;
        public HealthHandler(float health) 
        {
            _totalHealth = health;
            _currentHealth = health;
        }

        public void RemoveHealth(float damage)
        {
            _currentHealth = Mathf.Max(0f, _currentHealth - damage);
            onHealthChange?.Invoke(_totalHealth, _currentHealth);
        }
    }
}
