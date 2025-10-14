using CEVerticalShooter.Core.Save;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Score;
using CEVerticalShooter.Game.WinCondition;
using System;
using VContainer.Unity;

namespace CEVerticalShooter.Game
{
    public class GameService : IGameService, ITickable
    {
        private IScoreService _scoreService;
        private IDataService<GameData> _dataService;
        private IWinConditionService _winConditionService;
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
        public Action OnGameStart {  get; set; }

        public GameService(IScoreService scoreService, IDataService<GameData> dataService, IWinConditionService winConditionService, PlayerData playerData) 
        {
            _scoreService = scoreService;
            _dataService = dataService;
            _winConditionService = winConditionService;
            _playerData = playerData;
            ResetGame();
        }

        public void StartGame()
        {
            _isRunning = true;
            OnGameStart?.Invoke();
        }
        public void ResetGame()
        {
            _scoreService.ResetScore();
            _winConditionService.ResetWinConditionTracker();
            Lives = _playerData.Lives;
            _hasNewHighscore = false;
            OnNewGame?.Invoke();
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

        public void Tick()
        {
            if(!IsRunning)
                return;

            if(_winConditionService.HasReachedAnyWinCondition)
            {
                _isRunning = false;
                OnGameOver?.Invoke();
            }
        }
    }
}
