using CEVerticalShooter.Core.Scenes;
using CEVerticalShooter.Core.UI;
using CEVerticalShooter.Game.Score;
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
        private GameObject highscoreGameObject;
        [SerializeField]
        private Button playAgainButton;
        [SerializeField]
        private Button backToMenuButton;

        private IScoreService _scoreService;
        private ISceneService _sceneService;
        private IGameService _gameService;

        [Inject]
        private void Construct(IScoreService scoreService, ISceneService sceneService, IGameService gameService)
        {
            _scoreService = scoreService;
            _sceneService = sceneService;
            _gameService = gameService;
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
