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
            var input = context.PlayerInputHandler;

            if (input.MoveInput.magnitude < 0.1f)
            {
                context.ChangeState(context.PlayerIdleState);
                return;
            }
            
            context.PlayerMovement.Move(input.MoveInput);
            
        }

        public void OnExit(PlayerStateMachine context)
        {
            throw new System.NotImplementedException();
        }
    }
}