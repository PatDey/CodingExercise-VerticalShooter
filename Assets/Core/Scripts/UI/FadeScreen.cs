using UnityEngine;

namespace CEVerticalShooter.Core.UI
{
    [RequireComponent(typeof(Animator))]
    public abstract class FadeScreen : MonoBehaviour
    {
        private Animator _fadeAnimator;

        private const string ISON = "IsOn";

        public virtual void Awake()
        {
            _fadeAnimator = GetComponent<Animator>();
        }

        public void Show() => _fadeAnimator.SetBool(ISON, true);
        public void Hide() => _fadeAnimator.SetBool(ISON, false);
    }
}
