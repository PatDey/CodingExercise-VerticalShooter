using UnityEngine;
using VContainer;
using VContainer.Unity;
using CEVerticalShooter.Game.Player;
using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Enemy;
using CEVerticalShooter.Game.Score;

namespace CEVerticalShooter.Game
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [Header("Components")]
        [SerializeField] BulletPoolHolder bulletPoolHolder;
        [SerializeField] EnemyPoolHolder enemyPoolHolder;
        [SerializeField] PlayArea playArea;

        [Header("Settings")]
        [SerializeField]
        private DataCollection dataCollection;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerHandler>(Lifetime.Singleton).WithParameter(dataCollection.PlayerData).WithParameter(dataCollection.BulletDataCollection);
            builder.Register<ScoreService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterComponent(playArea);
            builder.RegisterComponent(dataCollection);
            builder.RegisterComponent(bulletPoolHolder).WithParameter(dataCollection.BulletDataCollection);
            builder.RegisterComponent(enemyPoolHolder).WithParameter(dataCollection.EnemyDataCollection);
        }
    }
}
