using UnityEngine;

namespace CEVerticalShooter.Game.Bullet
{
    public class BulletController : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        public void Initialize(BulletData data)
        {
            _speed = data.Speed;
            _damage = data.Damage;
        }

        private void Update()
        {
            transform.position += transform.up * Time.deltaTime * _speed;
        }
    }
}
