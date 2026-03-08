using UnityEngine;

namespace CharacterSystems.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float CurrentSpeed { get; private set; }
        public float speed = 6f;
        public float rotationSpeed = 10f;

        [Header("Dash")]
        public float dashSpeed = 18f;
        public float dashDuration = 0.15f;
        public float dashCooldown = 0.5f;

        private CharacterController _controller;
        private PlayerInputHandler _input;

        private Vector3 _lastMoveDirection;
        private bool _isDashing;
        private float _dashTimer;
        private float _lastDashTime;

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputHandler>();
        }

        void Update()
        {
            HandleDash();

            if (_isDashing)
            {
                _controller.Move(_lastMoveDirection * (dashSpeed * Time.deltaTime));
                return;
            }

            Move();
        }

        void Move()
        {
            Vector2 moveInput = _input.MoveInput;

            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized;

            if (move.magnitude > 0.1f)
            {
                _lastMoveDirection = move;
                RotateTowards(move);
            }

            _controller.Move(move * (speed * Time.deltaTime));
            CurrentSpeed = move.magnitude;
        }

        void HandleDash()
        {
            if (_input.DashPressed && Time.time > _lastDashTime + dashCooldown)
            {
                StartDash();
            }

            if (!_isDashing) return;
            _dashTimer -= Time.deltaTime;

            if (_dashTimer <= 0)
            {
                _isDashing = false;
            }
        }

        void StartDash()
        {
            _isDashing = true;
            _dashTimer = dashDuration;
            _lastDashTime = Time.time;
        }

        void RotateTowards(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
