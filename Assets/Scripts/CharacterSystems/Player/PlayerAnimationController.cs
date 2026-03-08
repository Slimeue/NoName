using UnityEngine;

namespace CharacterSystems.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private PlayerMovement _movement;

        private readonly int _moveSpeedHash = Animator.StringToHash("Move");
        private readonly int _dashHash = Animator.StringToHash("Dash");

        void Awake()
        {
            _animator = GetComponent<Animator>();
            if(!_animator)
                _animator = GetComponentInChildren<Animator>();
            _movement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            UpdateMovementAnimation();
        }

        void UpdateMovementAnimation()
        {
            float speed = _movement.CurrentSpeed;

            _animator.SetFloat(_moveSpeedHash, speed);
        }

        public void PlayDash()
        {
            _animator.SetTrigger(_dashHash);
        }
    }
}
