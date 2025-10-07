using CEVerticalShooter.Core.Scenes;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CEVerticalShooter.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Button startGameButton;

        [Header("Settings")]
        [SerializeField] 
        private SceneID gameSceneID;

        private ISceneService _sceneService;

        [Inject]
        private void Construct(ISceneService sceneService)
        {
            _sceneService = sceneService;
        }

        private void Awake()
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _sceneService.LoadScene(gameSceneID);
        }
    }
}
