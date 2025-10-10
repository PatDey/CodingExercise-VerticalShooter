using CEVerticalShooter.Game.Bullet;
using CEVerticalShooter.Game.Data;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
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

        [Header("Settings")]
        [SerializeField]
        private float timeBetweenSpawns;

        private readonly CancellationTokenSource _tokenSource = new();
        private EnemyDataCollection _enemyDataCollection;
        private BulletDataCollection _bulletDataCollection;
        private EnemyPoolHolder _enemyPoolHolder;
        private BulletPoolHolder _bulletPoolHolder;
        private PlayArea _playArea;

        private float _lastSpawnTime;
        private bool _isSpawning;

        [Inject]
        private void Construct(DataCollection dataCollection, EnemyPoolHolder enemyPoolHolder, BulletPoolHolder bulletPoolHolder, PlayArea playArea)
        {
            _enemyDataCollection = dataCollection.EnemyDataCollection;
            _bulletDataCollection = dataCollection.BulletDataCollection;
            _enemyPoolHolder = enemyPoolHolder;
            _bulletPoolHolder = bulletPoolHolder;
            _playArea = playArea;
        }

        public void Update()
        {
            //only update when game is running

            if(_lastSpawnTime + timeBetweenSpawns < Time.time && !_isSpawning)
            {
                _isSpawning = true;
                SpawnEnemies().Forget();
            }


        }
        private async UniTask SpawnEnemies()
        {
            var enemies = Enum.GetValues(typeof(EnemyID));
            EnemyID randomEnemy = (EnemyID)Random.Range(0, enemies.Length);

            EnemyData data = _enemyDataCollection.GetDataWithID(randomEnemy);

            int groupSize = Random.Range(1, data.MaxGroupSize + 1);

            for (int i = 0; i < groupSize; i++)
            {
                FlightCurve flightCurve = flightCurves[Random.Range(0, flightCurves.Count)];
                Vector2 curveOffset = Vector2.Lerp(flightCurve.MinCurveOffset, flightCurve.MaxCurveOffset, Random.value);
                Vector3 startPosition = (Vector3)flightCurve.SplineContainer.EvaluatePosition(0) + (Vector3)curveOffset;

                EnemyController controller = await _enemyPoolHolder.GetPoolObjectWithIDAsync(randomEnemy, _tokenSource.Token);
                controller.transform.position = startPosition;
                EnemyHandler enemyHandler = new EnemyHandler(data, _bulletDataCollection, flightCurve, curveOffset);
                controller.Initialize(enemyHandler, _playArea, _bulletPoolHolder, _enemyPoolHolder);
                await UniTask.WaitForSeconds(data.SpawnIntervall, cancellationToken: _tokenSource.Token);
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
