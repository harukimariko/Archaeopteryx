using UniRx;
using UnityEngine;

namespace Archaeopteryx
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offsetPosition;

        private readonly ReactiveProperty<Transform> _targetRP = new ReactiveProperty<Transform>();

        private void Awake()
        {
            // Inspectorで設定された初期値を流す
            _targetRP.Value = _target;
        }

        private void Start()
        {
            // ターゲット変更時に追従開始
            _targetRP
                .Where(t => t != null)
                .Select(t => t.ObserveEveryValueChanged(x => x.position))
                .Switch() // ターゲット変更時に購読切り替え
                .Subscribe(pos =>
                {
                    var current = transform.position;
                    var target = new Vector3(
                        pos.x + _offsetPosition.x,
                        pos.y + _offsetPosition.y,
                        transform.position.z + _offsetPosition.z);

                    transform.position = Vector3.Lerp(current, target, 0.1f);
                    Debug.Log(transform.position);
                }).AddTo(this);
        }

        // 外部からアタッチ可能
        public void SetTarget(Transform newTarget)
        {
            _targetRP.Value = newTarget;
        }
    }
}