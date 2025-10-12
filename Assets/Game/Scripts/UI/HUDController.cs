using CEVerticalShooter.Game.Score;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using VContainer;

namespace CEVerticalShooter.Game.UI
{
    public class HUDController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TextMeshProUGUI livesText;

        private IScoreService _scoreService;
        private IGameService _gameService;

        [Inject]
        private void Construct(IScoreService scoreService, IGameService gameService)
        {
            _scoreService = scoreService;
            _gameService = gameService;
        }

        private void Awake()
        {
            _scoreService.OnScoreChange += ScoreService_OnScoreChange;
            _gameService.OnLivesChange += GameService_OnLivesChange;
            ScoreService_OnScoreChange(_scoreService.Score);
            GameService_OnLivesChange(_gameService.Lives);
        }

        private void OnDestroy()
        {
            _scoreService.OnScoreChange -= ScoreService_OnScoreChange;
        }

        private void ScoreService_OnScoreChange(int score)
        {
            scoreText.text = score.ToString();
        }
        private void GameService_OnLivesChange(int lives)
        {
            livesText.text = lives.ToString();
        }
    }
}
