using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace CEVerticalShooter.Core.Scenes
{
    public interface ISceneService
    {
        public event Action OnBeforeSceneChange;
        public event Action OnAfterSceneChange;
        public void LoadScene(SceneID scene);
        public UniTask LoadSceneAsync(SceneID scene, CancellationToken cancellationToken);
    }
}
