using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace CEVerticalShooter.Core.Scenes
{
    public class AddressablesSceneService : ISceneService, IDisposable
    {
        private readonly CancellationTokenSource _tokenSource = new();
        private SceneSettings _settings;
        private SceneInstance _currentScene;

        public event Action OnBeforeSceneChange;
        public event Action OnAfterSceneChange;
        public AddressablesSceneService(SceneSettings settings) 
        {
            _settings = settings;
        }

        public void LoadScene(SceneID scene)
        {
            LoadSceneAsync(scene, _tokenSource.Token).Forget();
        }

        public async UniTask LoadSceneAsync(SceneID scene, CancellationToken cancellationToken)
        {
            OnBeforeSceneChange?.Invoke();

            await Addressables.InitializeAsync();

            if(_currentScene.Scene.isLoaded)
            {
                await Addressables.UnloadSceneAsync(_currentScene);
            }

            await Resources.UnloadUnusedAssets();

            AssetReference sceneToLoad =_settings.GetSceneAsset(scene);
            _currentScene = await Addressables.LoadSceneAsync(sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Additive);

            OnAfterSceneChange?.Invoke();
        }
        
        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
