using Interface;
using UnityEngine;

namespace CharacterSystems.Player.PlayerStates
{
    public class PlayerAttackState : IState<PlayerStateMachine>
    {
        private PlayerStateMachine _playerStateMachine;

        public void OnEnter(PlayerStateMachine context)
        {
            _playerStateMachine = context;

            var isMoving = _playerStateMachine.PlayerInputHandler.MoveInput.magnitude > 0.1f;

            _playerStateMachine.PlayerAttackComponent.StartAttack(isMoving,
                _playerStateMachine.PlayerAnimationController);
            _playerStateMachine.PlayerAnimationController.PlayAttack();

            // unsubscribe first to avoid duplicate calls
            _playerStateMachine.PlayerAnimationController.OnAttackFinished += OnAttackComplete;
        }

        public void OnUpdate(PlayerStateMachine context)
        {
            if (context.PlayerInputHandler.AttackPressed) context.PlayerAttackComponent.QueueNextAttack();
        }

        public void OnExit(PlayerStateMachine context)
        {
            Debug.Log("Attack End");
            // ← should unsubscribe here!
            context.PlayerAnimationController.OnAttackFinished -= OnAttackComplete;
        }

        // PlayerAttackState.cs
        private void OnAttackComplete()
        {
            var comboContinued = _playerStateMachine.PlayerAttackComponent.OnAttackFinished();

            if (!comboContinued)
                SwitchState(_playerStateMachine.PlayerIdleState); // only go idle if no combo
        }

        public void SwitchState(IState<PlayerStateMachine> stateMachine)
        {
            _playerStateMachine.ChangeState(stateMachine);
        }
    }
}