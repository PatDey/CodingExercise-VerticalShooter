using System;
using UnityEngine;

namespace CEVerticalShooter.Core.Save
{
    [Serializable]
    public class GameData
    {
        [SerializeField]
        private float _highscore;
        public float HighScore
        {
            get => _highscore; 
            set => _highscore = value;
        }

        public GameData()
        {
            _highscore = 0;
        }
    }
}
