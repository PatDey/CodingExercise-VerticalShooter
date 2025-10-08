using UnityEngine;
using VContainer;
using VContainer.Unity;
using CEVerticalShooter.Game.Player;
using CEVerticalShooter.Game.Bullet;

namespace CEVerticalShooter.Game
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [Header("Components")]
        [SerializeField] BulletPoolHolder bulletPoolHolder;

        [Header("Settings")]
        [SerializeField]
        private PlayerSettings playerSettings;
        [SerializeField]
        private DataCollection dataCollection;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerHandler>(Lifetime.Singleton).WithParameter(playerSettings).AsImplementedInterfaces();
            builder.RegisterComponent(dataCollection);
            builder.RegisterComponent(bulletPoolHolder).WithParameter(dataCollection);
        }
    }
}
