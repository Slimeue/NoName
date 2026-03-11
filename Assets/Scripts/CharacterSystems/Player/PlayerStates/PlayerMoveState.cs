using Interface;
using UnityEngine;

namespace CharacterSystems.Player.PlayerStates
{
    public class PlayerMoveState : IState<PlayerStateMachine>
    {
        public void OnEnter(PlayerStateMachine context)
        {
            Debug.Log("Entered: " + this);
        }

        public void OnUpdate(PlayerStateMachine context)
        {
            var input = context.PlayerInputHandler.MoveInput;

            context.PlayerMovement.Move(input);

            context.PlayerAnimationController.SetMoveSpeed(
                context.PlayerMovement.CurrentSpeed
            );

            if(context.PlayerInputHandler.AttackPressed) context.ChangeState(context.PlayerAttackState);
            if (input.magnitude < 0.1f) context.ChangeState(context.PlayerIdleState);
        }

        public void OnExit(PlayerStateMachine context)
        {
        }
    }
}