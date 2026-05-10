using UnityEngine;
using UnityEngine.InputSystem;

namespace Archaeopteryx
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float moveSpeed = 6f;

        private InputAction moveAction;
        private Vector2 moveInput;

        private void Awake()
        {
            if (rb == null)
                rb = GetComponent<Rigidbody>();

            // Input Actionをコードで作る
            moveAction = new InputAction("Move", InputActionType.Value);

            // WASDバインド
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
        }

        private void OnEnable()
        {
            moveAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
        }

        private void Update()
        {
            moveInput = moveAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 dir = new Vector3(moveInput.x, moveInput.y, 0f);

            Vector3 velocity = rb.linearVelocity;
            velocity.x = dir.x * moveSpeed;
            velocity.y = dir.y * moveSpeed;

            rb.linearVelocity = velocity;
        }
    }
}