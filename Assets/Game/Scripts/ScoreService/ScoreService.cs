using System;
using UnityEngine;

namespace CEVerticalShooter.Game.Score
{
    public class ScoreService : IScoreService
    {
        private int _currentScore;
        public ScoreService() 
        {
            _currentScore = 0;
        }
        public int Score => _currentScore;

        public Action<int> OnScoreChange { get; set; }

        public void AddScore(int scoreToAdd)
        {    
            _currentScore += scoreToAdd;
            OnScoreChange?.Invoke(_currentScore);

        }

        public void ResetScore()
        {
            _currentScore = 0;
            OnScoreChange?.Invoke(_currentScore);
        }
    }
}
