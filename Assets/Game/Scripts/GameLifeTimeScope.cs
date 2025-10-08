using UnityEngine;
using VContainer;
using VContainer.Unity;
using CEVerticalShooter.Game.Player;

namespace CEVerticalShooter.Game
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [Header("Settings")]
        [SerializeField]
        private PlayerSettings playerSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerHandler>(Lifetime.Singleton).WithParameter(playerSettings).AsImplementedInterfaces();
        }
    }
}
