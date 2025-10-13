using CEVerticalShooter.Core.Save;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Score;
using System;

namespace CEVerticalShooter.Game
{
    public class GameService : IGameService
    {
        private IScoreService _scoreService;
        private IDataService<GameData> _dataService;
        private PlayerData _playerData;

        private bool _isRunning;
        private int _lives;
        private bool _hasNewHighscore;
        public bool IsRunning => _isRunning;
        public bool HasNewHighscore => _hasNewHighscore;

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

        public GameService(IScoreService scoreService, IDataService<GameData> dataService, PlayerData playerData) 
        {
            _scoreService = scoreService;
            _dataService = dataService;
            _playerData = playerData;
            StartGame();
        }

        public void StartGame()
        {
            _scoreService.ResetScore();
            Lives = _playerData.Lives;
            _hasNewHighscore = false;
            OnNewGame?.Invoke();
            _isRunning = true;
        }

        public void ReduceLife()
        {
            Lives--;

            if(Lives == 0)
            {
                _isRunning = false;

                if(_dataService.Data.HighScore <= _scoreService.Score)
                {
                    _dataService.Data.HighScore = _scoreService.Score;
                    _dataService.Save();
                    _hasNewHighscore = true;
                }

                OnGameOver?.Invoke();
            }
        }
    }
}
