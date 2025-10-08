using CEVerticalShooter.Game.Bullet;
using UnityEngine;

namespace CEVerticalShooter.Game
{
    [CreateAssetMenu(fileName = "DataCollection", menuName = "ScriptableObjects/DataCollection", order = 1)]
    public class DataCollection : ScriptableObject
    {
        [SerializeField]
        private BulletDataCollection bulletDataCollection;

        public BulletDataCollection BulletDataCollection => bulletDataCollection;
    }
}
