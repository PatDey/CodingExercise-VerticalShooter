using CEVerticalShooter.Core.UI;
using CEVerticalShooter.Game;
using VContainer;

namespace CEVerticalShooter
{
    public class StartTextScreen : FadeScreen
    {
        private IGameService _gameService;

        [Inject]
        private void Construct(IGameService gameService)
        {
            _gameService = gameService;
        }

        public override void Awake()
        {
            base.Awake();
            _gameService.OnNewGame += GameService_OnNewGame;
            _gameService.OnGameStart += GameService_OnGameStart;
            GameService_OnNewGame();
        }

        public void OnDestroy()
        {
            if(_gameService != null)
            { 
                _gameService.OnNewGame -= GameService_OnNewGame;
                _gameService.OnGameStart -= GameService_OnGameStart;
            }
        }

        private void GameService_OnNewGame() => Show();
        private void GameService_OnGameStart() => Hide();
    }
}
