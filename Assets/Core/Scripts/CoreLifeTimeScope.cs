using CEVerticalShooter.Core.Save;
using CEVerticalShooter.Core.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CEVerticalShooter.Core
{
    public class CoreLifeTimeScope : LifetimeScope
    {
        [Header("Settings")]
        [SerializeField] private SceneSettings sceneSettings;

        [Header("Initialization Settings")]
        [SerializeField] private SceneID startSceneID;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AddressablesSceneService>(Lifetime.Singleton).WithParameter(sceneSettings).AsImplementedInterfaces();
            builder.Register<DataService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<CoreEntryPoint>().WithParameter(startSceneID);
        }
    }
}
