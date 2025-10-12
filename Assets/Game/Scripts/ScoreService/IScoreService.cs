using System;
using UnityEngine;

namespace CEVerticalShooter.Game.Score
{
    public interface IScoreService
    {
        public int Score { get; }
        public Action<int> OnScoreChange { get; set; }
        public void AddScore(int scoreToAdd);
        public void ResetScore();
    }
}
