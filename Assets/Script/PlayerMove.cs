using UniRx;
using UnityEngine;

namespace Archaeopteryx
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Camera _mainCamera;

        [SerializeField] private Transform _crosshair;

        [Header("Move")]
        [SerializeField] private float _followSpeed = 5f;

        [SerializeField, Range(0f, 4f)]
        private float _betweenRate = 2f;

        [Header("Rotation")]
        [SerializeField] private float _rotateSpeed = 5f;

        private void Start()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    Move();
                    Rotate();
                })
                .AddTo(this);
        }

        private void Move()
        {
            Vector3 target = Vector3.Lerp(
                _mainCamera.transform.position,
                _crosshair.position,
                _betweenRate
            );

            target.z = transform.position.z;

            transform.position = Vector3.Lerp(
                transform.position,
                target,
                _followSpeed * Time.deltaTime
            );
        }

        private void Rotate()
        {
            Vector3 direction =
                _crosshair.position - transform.position;

            if (direction == Vector3.zero)
                return;

            Quaternion targetRot =
                Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRot,
                _rotateSpeed * Time.deltaTime
            );
        }
    }
}