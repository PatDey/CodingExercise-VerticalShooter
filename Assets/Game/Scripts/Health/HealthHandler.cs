using System;
using UnityEngine;

namespace CEVerticalShooter.Game.Health
{
    public class HealthHandler
    {
        private float _totalHealth;
        private float _currentHealth;
        public float TotalHealth => _totalHealth;
        public float CurrentHealth 
        {
            get => _currentHealth;

            private set
            {
                _currentHealth = value;
                OnHealthChange?.Invoke(_totalHealth, _currentHealth);
            }
        }
        public bool IsDead => CurrentHealth == 0;

        public Action<float, float> OnHealthChange;
        public HealthHandler(float health) 
        {
            _totalHealth = health;
            CurrentHealth = health;
        }
        public void RemoveHealth(float damage) => CurrentHealth = Mathf.Max(0f, _currentHealth - damage);
        public void ResetHealth() => CurrentHealth = _totalHealth;
    }
}
