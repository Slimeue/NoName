using Interface;
using UnityEngine;

namespace CharacterSystems.Player.PlayerStates
{
    public class PlayerIdleState : IState<PlayerStateMachine>
    {
        public void OnEnter(PlayerStateMachine context)
        {
            Debug.Log("Entered PlayerIdleState");
        }

        public void OnUpdate(PlayerStateMachine context)
        {
            var input = context.PlayerInputHandler;

            if (input.MoveInput.magnitude > 0.1f)
            {
                context.ChangeState(context.PlayerMoveState);
                return;
            }

            // if (input.AttackPressed)
            // {
            //     context.ChangeState(context.PlayerAttackState);
            // }
        }

        public void OnExit(PlayerStateMachine context)
        {
            Debug.Log("Exited PlayerIdleState");
        }
    }
}