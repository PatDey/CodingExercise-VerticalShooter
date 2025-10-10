using CEVerticalShooter.Game.Data;
using Cysharp.Threading.Tasks;
using System.Threading;
using VContainer;

namespace CEVerticalShooter.Game.Bullet
{
    public class BulletPoolHolder : PoolHolder<BulletDataCollection, BulletData, BulletID, BulletPool, BulletController>
    {
        [Inject]
        public override void Construct(BulletDataCollection dataCollection)
        {
            _dataCollection = dataCollection;
        }
    }
}
