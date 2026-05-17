using UniRx;
using UnityEngine;

namespace Archaeopteryx
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private Transform _target;

        [SerializeField] private Vector3 _offsetPosition;

        [SerializeField, Range(0.01f, 15.0f)]
        private float _followSmooth = 0.1f;

        [Header("Dead Zone")]
        [SerializeField]
        private bool _isDeadZone = false;

        [SerializeField]
        private Vector2 _deadZoneMin = new Vector2(0.4f, 0.4f);

        [SerializeField]
        private Vector2 _deadZoneMax = new Vector2(0.6f, 0.6f);

        private void Start()
        {
            Observable.EveryLateUpdate()
                .Subscribe(_ => Move())
                .AddTo(this);
        }

        private void Move()
        {
            if (_target == null)
                return;

            // =========================
            // Dead Zone
            // =========================

            Vector3 viewportPos =
                _camera.WorldToViewportPoint(
                    _target.position
                );

            bool insideDeadZone =
                viewportPos.x > _deadZoneMin.x &&
                viewportPos.x < _deadZoneMax.x &&
                viewportPos.y > _deadZoneMin.y &&
                viewportPos.y < _deadZoneMax.y;

            if (_isDeadZone && insideDeadZone)
                return;

            // =========================
            // Target Position
            // =========================

            Vector3 targetPos =
                _target.position + _offsetPosition;

            Vector3 currentPos =
                transform.position;

            // XだけLerp
            currentPos.x = Mathf.Lerp(
                currentPos.x,
                targetPos.x,
                _followSmooth * Time.deltaTime
            );

            // YだけLerp
            currentPos.y = Mathf.Lerp(
                currentPos.y,
                targetPos.y,
                _followSmooth * Time.deltaTime
            );

            // Zは一定距離間
            currentPos.z = targetPos.z;

            // Zは変更しない
            transform.position = currentPos;
        }
    }
}