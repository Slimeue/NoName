using UnityEngine;

namespace CharacterSystems.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public delegate void AttackFinished();
        public event AttackFinished OnAttackFinished;

        private static readonly int MoveHash = Animator.StringToHash("Move");
        private static readonly int DashHash = Animator.StringToHash("Dash");
        private static readonly int AttackHash = Animator.StringToHash("Attack");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (!_animator)
                _animator = GetComponentInChildren<Animator>();
        }

        public void SetMoveSpeed(float speed)
        {
            _animator.SetFloat(MoveHash, speed);
        }

        public void PlayDash()
        {
            _animator.SetTrigger(DashHash);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(AttackHash);
        }

        public void OverrideController(AnimatorOverrideController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }

        public void AttackFinishedEvent()
        {
            Debug.Log("OnAttackFinished Event");
            OnAttackFinished?.Invoke();
        }
    }
}