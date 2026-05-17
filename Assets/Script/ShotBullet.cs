using UnityEngine;
using UnityEngine.InputSystem;

namespace Archaeopteryx
{
    public class ShotBullet : MonoBehaviour
    {
        [Header("Pool")]
        [SerializeField] private BulletPool _bulletPool;

        [Header("Shot Point")]
        [SerializeField] private Transform _muzzle;

        private InputAction _shotAAction;
        private InputAction _shotBAction;

        [SerializeField]
        private Transform _crosshair;

        private void Awake()
        {
            // =========================
            // ShotA
            // Space / 左クリック
            // =========================

            _shotAAction = new InputAction(
                name: "ShotA",
                type: InputActionType.Button
            );

            _shotAAction.AddBinding("<Keyboard>/space");
            _shotAAction.AddBinding("<Mouse>/leftButton");

            // =========================
            // ShotB
            // 右クリック
            // =========================

            _shotBAction = new InputAction(
                name: "ShotB",
                type: InputActionType.Button
            );

            _shotBAction.AddBinding("<Mouse>/rightButton");
        }

        private void OnEnable()
        {
            _shotAAction.Enable();
            _shotBAction.Enable();

            _shotAAction.performed += OnShotA;
            _shotBAction.performed += OnShotB;
        }

        private void OnDisable()
        {
            _shotAAction.performed -= OnShotA;
            _shotBAction.performed -= OnShotB;

            _shotAAction.Disable();
            _shotBAction.Disable();
        }

        private void OnShotA(InputAction.CallbackContext context)
        {
            ShotA();
        }

        private void OnShotB(InputAction.CallbackContext context)
        {
            ShotB();
        }

        private void ShotA()
        {
            GameObject bullet =
                _bulletPool.GetBullet();

            if (bullet == null)
                return;

            bullet.transform.position =
                _muzzle.position;

            // 照準方向
            Vector3 direction = (_crosshair.position - _muzzle.position).normalized;

            // 向き設定
            bullet.transform.forward = direction;

            // 弾へ方向を渡す
            BulletA bulletA =
                bullet.GetComponent<BulletA>();

            bulletA.SetDirection(direction);
        }

        private void ShotB()
        {
            Debug.Log("ShotB");
        }
    }
}