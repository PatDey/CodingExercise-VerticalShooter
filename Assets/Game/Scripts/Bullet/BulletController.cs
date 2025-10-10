using CEVerticalShooter.Game.Data;
using UnityEngine;

namespace CEVerticalShooter.Game.Bullet
{
    public class BulletController : MonoBehaviour
    {
        private BulletPoolHolder _bulletPoolHolder;
        private BulletData _data;
        private PlayArea _playArea;
        public void Initialize(BulletPoolHolder bulletPoolHolder, BulletData data, PlayArea playArea)
        {
            _data = data;
            _bulletPoolHolder = bulletPoolHolder;
            _playArea = playArea;
        }

        private void Update()
        {
            transform.position += transform.up * Time.deltaTime * _data.Speed;

            if(!_playArea.IsPositionInPlayArea(transform.position))
                _bulletPoolHolder.ReturnPoolObjectWithID(_data.ID, this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlaneController planeController = collision.gameObject.GetComponent<PlaneController>();
            if(planeController)
            {
                _bulletPoolHolder.ReturnPoolObjectWithID(_data.ID, this);
            }
        }
    }
}
