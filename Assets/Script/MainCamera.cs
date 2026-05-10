using UniRx;
using UnityEngine;

namespace Archaeopteryx
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offsetPosition;

        private readonly ReactiveProperty<Transform> _targetRP = new ReactiveProperty<Transform>();

        private Vector3 _targetPos;

        [SerializeField, Range(0.01f, 1f)]
        private float _followSmooth = 0.1f;

        private void Awake()
        {
            // Inspector初期値を反映
            _targetRP.Value = _target;
        }

        private void Start()
        {
            _targetRP
                .Where(t => t != null)
                .Select(t => t.ObserveEveryValueChanged(x => x.position))
                .Switch()
                .Subscribe(pos =>
                {
                    // オフセットは必ずターゲット基準で統一
                    _targetPos = pos + _offsetPosition;
                })
                .AddTo(this);
        }

        private void LateUpdate()
        {
            // カメラ追従は物理更新後にやる（重要）
            transform.position = Vector3.Lerp(
                transform.position,
                _targetPos,
                _followSmooth
            );
        }

        // 外部からターゲット差し替え可能
        public void SetTarget(Transform newTarget)
        {
            _targetRP.Value = newTarget;
        }
    }
}