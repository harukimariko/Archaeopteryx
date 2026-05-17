using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Archaeopteryx
{
    public class PlayerForwardMove : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float _speed = 5.0f;
        [Header("Player")]
        [SerializeField] private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = transform;

            Observable.EveryFixedUpdate()
                .Subscribe(_ =>
                {
                    Move();
                });
        }

        private void Move()
        {
            transform.position += _playerTransform.forward * _speed * Time.deltaTime;
        }
    }
}