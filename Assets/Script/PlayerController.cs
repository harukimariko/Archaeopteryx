using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Archaeopteryx
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera;

        [SerializeField]
        private float _moveSpeed = 6f;

        [Header("Viewport Clamp")]
        [SerializeField]
        private float _minX = 0.1f;

        [SerializeField]
        private float _maxX = 0.9f;

        [SerializeField]
        private float _minY = 0.1f;

        [SerializeField]
        private float _maxY = 0.9f;

        private InputAction _moveAction;

        private void Awake()
        {
            _moveAction = new InputAction("Move");

            _moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
        }

        private void OnEnable()
        {
            _moveAction.Enable();

            Observable.EveryUpdate()
                .Subscribe(_ => Move())
                .AddTo(this);
        }

        private void OnDisable()
        {
            _moveAction.Disable();
        }

        private void Move()
        {
            Vector2 input =
                _moveAction.ReadValue<Vector2>();

            Vector3 move =
                new Vector3(input.x, input.y, 0f);

            transform.position +=
                move *
                _moveSpeed *
                Time.deltaTime;

            ClampViewport();
        }

        private void ClampViewport()
        {
            // World → Viewport
            Vector3 viewportPos =
                _mainCamera.WorldToViewportPoint(
                    transform.position
                );

            // Clamp
            viewportPos.x =
                Mathf.Clamp(
                    viewportPos.x,
                    _minX,
                    _maxX
                );

            viewportPos.y =
                Mathf.Clamp(
                    viewportPos.y,
                    _minY,
                    _maxY
                );

            // Viewport → World
            Vector3 worldPos =
                _mainCamera.ViewportToWorldPoint(
                    viewportPos
                );

            transform.position = worldPos;
        }
    }
}