using System.Collections.Generic;
using UnityEngine;

namespace Archaeopteryx
{
    public class BulletPool : MonoBehaviour
    {
        [Header("Bullet Prefab")]
        [SerializeField] private GameObject _bulletPrefab;

        [Header("Pool Size")]
        [SerializeField] private int _poolSize = 100;

        private readonly Queue<GameObject> _pool = new Queue<GameObject>();

        public Transform BulletTransform => _bulletPrefab.transform;

        private void Awake()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject bullet =
                    Instantiate(
                        _bulletPrefab,
                        transform
                    );

                bullet.SetActive(false);

                // BulletA 初期化
                BulletA bulletA =
                    bullet.GetComponent<BulletA>();

                if (bulletA != null)
                {
                    bulletA.Initialize(this);
                }

                _pool.Enqueue(bullet);
            }
        }

        public GameObject GetBullet()
        {
            if (_pool.Count <= 0)
            {
                Debug.LogWarning("BulletPool is Empty");

                return null;
            }

            GameObject bullet = _pool.Dequeue();

            bullet.SetActive(true);

            return bullet;
        }

        public void ReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);

            _pool.Enqueue(bullet);
        }
    }
}