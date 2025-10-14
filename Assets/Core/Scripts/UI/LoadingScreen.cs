using CEVerticalShooter.Core.Scenes;
using VContainer;

namespace CEVerticalShooter.Core.UI
{
    public class LoadingScreen : FadeScreen
    {
        private ISceneService _sceneService;

        [Inject]
        private void Construct(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        public override void Awake()
        {
            base.Awake();
            _sceneService.OnBeforeSceneChange += SceneService_OnBeforeSceneChange;
            _sceneService.OnAfterSceneChange += SceneService_OnAfterSceneChange;
        }

        public void OnDestroy()
        {
            if(_sceneService != null)
            { 
                _sceneService.OnBeforeSceneChange -= SceneService_OnBeforeSceneChange;
                _sceneService.OnAfterSceneChange -= SceneService_OnAfterSceneChange;
            }
        }

        private void SceneService_OnBeforeSceneChange() => Show();
        private void SceneService_OnAfterSceneChange() => Hide();
    }
}
