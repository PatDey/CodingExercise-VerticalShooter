using CEVerticalShooter.Game.Data;
using VContainer;

namespace CEVerticalShooter.Game.Enemy
{
    public class EnemyPoolHolder : PoolHolder<EnemyDataCollection, EnemyData, EnemyID, EnemyPool, PlaneController>
    {
        [Inject]
        public override void Construct(EnemyDataCollection dataCollection)
        {
            _dataCollection = dataCollection;
        }
    }
}
