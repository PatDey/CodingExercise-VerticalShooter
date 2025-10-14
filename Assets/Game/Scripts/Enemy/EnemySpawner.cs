using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using CEVerticalShooter.Game.Score;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemySpawner : MonoBehaviour, IDisposable
    {
        [Header("Components")]
        [SerializeField]
        private List<FlightCurve> flightCurves;

        private CancellationTokenSource _tokenSource = new();
        private EnemyDataCollection _enemyDataCollection;
        private BulletDataCollection _bulletDataCollection;
        private EnemyPoolHolder _enemyPoolHolder;
        private BulletPoolHolder _bulletPoolHolder;
        private PlayArea _playArea;
        private IScoreService _scoreService;
        private IGameService _gameService;
        private WinConditionTracker _winConditionTracker;

        private float _lastSpawnTime;
        private bool _isSpawning;

        [Inject]
        private void Construct(DataCollection dataCollection, EnemyPoolHolder enemyPoolHolder, BulletPoolHolder bulletPoolHolder, PlayArea playArea, 
                                WinConditionTracker winConditionTracker, IScoreService scoreService, IGameService gameService)
        {
            _enemyDataCollection = dataCollection.EnemyDataCollection;
            _bulletDataCollection = dataCollection.BulletDataCollection;
            _enemyPoolHolder = enemyPoolHolder;
            _bulletPoolHolder = bulletPoolHolder;
            _playArea = playArea;
            _scoreService = scoreService;
            _gameService = gameService;
            _winConditionTracker = winConditionTracker;
        }

        private void Awake()
        {
            _gameService.OnGameOver += GameService_OnGameOver;
        }

        private void OnDestroy()
        {
            if(_gameService != null)
                _gameService.OnGameOver += GameService_OnGameOver;
        }

        private void GameService_OnGameOver()
        {
            _tokenSource.Cancel();
            _tokenSource.Dispose();
            _tokenSource = new CancellationTokenSource();
        }

        public void Update()
        {
            if (_gameService.IsRunning) 
            {
                if(_lastSpawnTime + _enemyDataCollection.TimeBetweenSpawns < Time.time && !_isSpawning)
                {
                    _isSpawning = true;
                    SpawnEnemies(_tokenSource.Token).Forget();
                }
            }
        }
        private async UniTask SpawnEnemies(CancellationToken token)
        {
            var enemies = Enum.GetValues(typeof(EnemyID));
            EnemyID randomEnemy = (EnemyID)Random.Range(0, enemies.Length);

            if(_enemyDataCollection.TryToGetDataWithID(randomEnemy, out EnemyData data))
            { 
                int groupSize = Random.Range(data.MinGroupSize, data.MaxGroupSize + 1);

                for (int i = 0; i < groupSize; i++)
                {
                    FlightCurve flightCurve = flightCurves[Random.Range(0, flightCurves.Count)];
                    Vector2 curveOffset = Vector2.Lerp(flightCurve.MinCurveOffset, flightCurve.MaxCurveOffset, Random.value);
                    Vector3 startPosition = (Vector3)flightCurve.SplineContainer.EvaluatePosition(0) + (Vector3)curveOffset;

                    EnemyController controller = await _enemyPoolHolder.GetPoolObjectWithIDAsync(randomEnemy, token);
                    controller.transform.position = startPosition;
                    EnemyHandler enemyHandler = new EnemyHandler(data, _bulletDataCollection, flightCurve, curveOffset);
                    controller.Initialize(enemyHandler, _playArea, _bulletPoolHolder, _enemyPoolHolder, _winConditionTracker, _scoreService, _gameService);
                    await UniTask.WaitForSeconds(data.SpawnIntervall, cancellationToken: token);
                }
            }

            _isSpawning = false;
            _lastSpawnTime = Time.time;
        }

        public void Dispose()
        {
            _tokenSource?.Dispose();
        }
    }
}
