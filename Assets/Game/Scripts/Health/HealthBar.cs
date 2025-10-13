using CEVerticalShooter.Game.Health;
using UnityEngine;
using UnityEngine.UI;

namespace CEVerticalShooter.Game.UI
{
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(Animator))]
    public class HealthBar : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float healthPercentToBlink;

        private Slider _healthBarSlider;
        private Animator _healthBarAnimator;
        private HealthHandler _healthHandler;

        private const string ISON = "IsOn";

        public void Initialize(HealthHandler healthHandler)
        {
            _healthHandler = healthHandler;
        }

        private void Awake()
        {
            _healthBarSlider = GetComponent<Slider>();
            _healthBarAnimator = GetComponent<Animator>();
            _healthBarSlider.maxValue = _healthHandler.TotalHealth;
            _healthBarSlider.value = _healthHandler.TotalHealth;
            _healthHandler.OnHealthChange += HealthHandler_OnHealthChange;
        }
        private void HealthHandler_OnHealthChange(float total, float current)
        {
            _healthBarSlider.value = current;
            _healthBarAnimator.SetBool(ISON, current/total <= healthPercentToBlink);
        }
    }
}
