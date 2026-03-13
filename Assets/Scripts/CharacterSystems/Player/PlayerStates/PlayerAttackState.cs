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
            
            _playerStateMachine.PlayerAttackComponent.Initialize(_playerStateMachine.PlayerAnimationController);

            Debug.Log("AttackEnter");
            _playerStateMachine.PlayerAttackComponent.StartAttack();
        }

        public void OnUpdate(PlayerStateMachine context)
        {
            if (_playerStateMachine.PlayerInputHandler.AttackPressed)
                _playerStateMachine.PlayerAttackComponent.UpdateAttack();
        }

        public void OnExit(PlayerStateMachine context)
        {
            Debug.Log("Attack End");
        }

        public void SwitchState(IState<PlayerStateMachine> stateMachine)
        {
            _playerStateMachine.ChangeState(stateMachine);
        }
    }
}
