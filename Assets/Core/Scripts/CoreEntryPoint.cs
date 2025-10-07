using CEVerticalShooter.Core.Scenes;
using Cysharp.Threading.Tasks;
using System.Threading;
using VContainer.Unity;

namespace CEVerticalShooter.Core
{
    public class CoreEntryPoint : IAsyncStartable
    {
        private ISceneService _sceneService;
        private LifetimeScope _currentScope;
        private SceneID _initialScene;

        public CoreEntryPoint(ISceneService sceneService, LifetimeScope currentScope, SceneID initialScene)
        {
            _sceneService = sceneService;
            _currentScope = currentScope;
            _initialScene = initialScene;
        }

        public virtual async UniTask StartAsync(CancellationToken cancellation)
        {
            using (LifetimeScope.EnqueueParent(_currentScope))
            {
                await _sceneService.LoadSceneAsync(_initialScene, cancellation);
            }
        }
    }
}
