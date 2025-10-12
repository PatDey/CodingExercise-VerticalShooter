using System;

namespace CEVerticalShooter.Game
{
    public interface IGameService
    {
        public bool IsRunning { get; }
        public int Lives{ get;}

        public Action<int> OnLivesChange { get; set; }
        public Action OnGameOver { get; set;}
        public Action OnNewGame { get; set;}
        public void StartGame();
        public void ReduceLife();
    }
}
