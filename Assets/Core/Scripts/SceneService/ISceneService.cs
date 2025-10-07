using Cysharp.Threading.Tasks;
using System.Threading;

namespace CEVerticalShooter.Core.Scenes
{
    public interface ISceneService
    {
        public void LoadScene(SceneID scene);
        public UniTask LoadSceneAsync(SceneID scene, CancellationToken cancellationToken);
    }
}
