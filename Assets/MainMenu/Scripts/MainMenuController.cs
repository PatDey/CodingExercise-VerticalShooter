using CEVerticalShooter.Core.Save;
using CEVerticalShooter.Core.Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CEVerticalShooter.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Button startGameButton;
        [SerializeField]
        private TextMeshProUGUI currentHighscoreText;

        [Header("Settings")]
        [SerializeField] 
        private SceneID gameSceneID;

        private ISceneService _sceneService;
        private IDataService<GameData> _dataService;

        [Inject]
        private void Construct(ISceneService sceneService, IDataService<GameData> dataService)
        {
            _sceneService = sceneService;
            _dataService = dataService;
        }

        private void Awake()
        {
            startGameButton.onClick.AddListener(StartGame);
            currentHighscoreText.text = _dataService.Data.HighScore.ToString();
        }

        private void StartGame()
        {
            _sceneService.LoadScene(gameSceneID);
        }
    }
}
