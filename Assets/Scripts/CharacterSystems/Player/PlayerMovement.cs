using UnityEngine;

namespace CharacterSystems.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 6f;
        public float rotationSpeed = 10f;

        [Header("Dash")] public float dashSpeed = 18f;

        public float dashDuration = 0.15f;
        public float dashCooldown = 0.5f;

        private CharacterController _controller;
        private float _dashTimer;
        private PlayerInputHandler _input;
        private bool _isDashing;
        private float _lastDashTime;

        private Vector3 _lastMoveDirection;
        public float CurrentSpeed { get; set; }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputHandler>();
        }

        private void Update()
        {
        }

        public void MoveUpdate()
        {
            // HandleDash();
        }

        public void Move(Vector2 input)
        {
            var move = new Vector3(input.x, 0, input.y).normalized;

            if (move.magnitude > 0.1f)
            {
                _lastMoveDirection = move;
                RotateTowards(move);
            }

            _controller.Move(move * (speed * Time.deltaTime));
            CurrentSpeed = move.magnitude;
        }

        public void HandleDash(bool dashPressed)
        {
            if (dashPressed && Time.time > _lastDashTime + dashCooldown) StartDash();

            if (!_isDashing) return;
            _dashTimer -= Time.deltaTime;

            if (_dashTimer <= 0) _isDashing = false;
        }

        private void RotateTowards(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }


        #region Dash Logics

        public bool CanDash => Time.time > _lastDashTime + dashCooldown;

        public void StartDash()
        {
            _isDashing = true;
            _dashTimer = dashDuration;
            _lastDashTime = Time.time;
        }

        public void DashMove()
        {
            _controller.Move(_lastMoveDirection * (dashSpeed * Time.deltaTime));
        }

        public void EndDash()
        {
        }

        #endregion
    }
}