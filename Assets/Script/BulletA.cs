using UniRx;
using UnityEngine;

namespace Archaeopteryx
{
    public class BulletA : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float _speed = 100f;

        [Header("Life Time")]
        [SerializeField] private float _lifeTime = 3f;

        private BulletPool _bulletPool;

        private Vector3 _moveDirection;

        public void Initialize(BulletPool pool)
        {
            _bulletPool = pool;
        }

        public void SetDirection(Vector3 direction)
        {
            _moveDirection = direction.normalized;
        }

        private void OnEnable()
        {
            Observable.Timer(
                    System.TimeSpan.FromSeconds(_lifeTime))
                .Subscribe(_ => ReturnToPool())
                .AddTo(this);
        }

        private void Update()
        {
            transform.position +=
                _moveDirection *
                _speed *
                Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (_bulletPool == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _bulletPool.ReturnBullet(gameObject);
        }
    }
}