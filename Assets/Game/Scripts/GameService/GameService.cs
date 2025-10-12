using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Score;
using System;

namespace CEVerticalShooter.Game
{
    public class GameService : IGameService
    {
        private IScoreService _scoreService;
        private PlayerData _playerData;

        private bool _isRunning;
        private int _lives;
        public bool IsRunning => _isRunning;

        public int Lives
        {
            get => _lives;

            private set
            {
                _lives = value;
                OnLivesChange?.Invoke(_lives);
            }
        }

        public Action<int> OnLivesChange { get; set; }
        public Action OnGameOver { get; set; }
        public Action OnNewGame {  get; set; }

        public GameService(IScoreService scoreService, PlayerData playerData) 
        {
            _scoreService = scoreService;
            _playerData = playerData;
            StartGame();
        }

        public void StartGame()
        {
            _scoreService.ResetScore();
            Lives = _playerData.Lives;
            OnNewGame?.Invoke();
            _isRunning = true;
        }

        public void ReduceLife()
        {
            Lives--;

            if(Lives == 0)
            {
                _isRunning = false;
                OnGameOver?.Invoke();
            }
        }
    }
}
