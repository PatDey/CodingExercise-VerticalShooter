using CEVerticalShooter.Core.Scenes;
using CEVerticalShooter.Core.UI;
using CEVerticalShooter.Game.Score;
using CEVerticalShooter.Game.WinCondition;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CEVerticalShooter.Game.UI
{
    public class GameOverScreen : FadeScreen
    {
        [Header("Components")]
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TextMeshProUGUI headerText;
        [SerializeField]
        private GameObject highscoreGameObject;
        [SerializeField]
        private Button playAgainButton;
        [SerializeField]
        private Button backToMenuButton;

        [Header("Settings")]
        [SerializeField]
        private string gameOverLoseText;
        [SerializeField]
        private string gameOverWinText;

        private IScoreService _scoreService;
        private ISceneService _sceneService;
        private IGameService _gameService;
        private IWinConditionService _winConditionService;

        [Inject]
        private void Construct(IScoreService scoreService, ISceneService sceneService, IGameService gameService, IWinConditionService winConditionService)
        {
            _scoreService = scoreService;
            _sceneService = sceneService;
            _gameService = gameService;
            _winConditionService = winConditionService;
        }

        public override void Awake()
        {
            base.Awake();
            backToMenuButton.onClick.AddListener(BackToMenuButton_OnClick);
            playAgainButton.onClick.AddListener(PlayAgainButton_OnClick);

            _gameService.OnGameOver += GameService_OnGameOver;
        }

        private void OnDestroy()
        {
            _gameService.OnGameOver -= GameService_OnGameOver;
        }

        private void GameService_OnGameOver()
        {
            headerText.text = _winConditionService.HasReachedAnyWinCondition ? gameOverWinText : gameOverLoseText;
            scoreText.text = _scoreService.Score.ToString();
            highscoreGameObject.SetActive(_gameService.HasNewHighscore);
            Show();
        }

        private void PlayAgainButton_OnClick()
        {
            _gameService.StartGame();
            Hide();
        }

        private void BackToMenuButton_OnClick()
        {
            _sceneService.LoadScene(SceneID.MainMenu);
        }

    }
}
