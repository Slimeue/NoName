using Interface;
using UnityEngine;

namespace CharacterSystems.Player.PlayerStates
{
    public class PlayerDashState : IState<PlayerStateMachine>
    {
        private float _dashTimer;

        public void OnEnter(PlayerStateMachine context)
        {
            _dashTimer = context.PlayerMovement.dashDuration;

            context.PlayerMovement.StartDash();
        }

        public void OnUpdate(PlayerStateMachine context)
        {
            context.PlayerMovement.DashMove();

            _dashTimer -= Time.deltaTime;

            if (_dashTimer <= 0) context.ChangeState(context.PlayerMoveState);
        }

        public void OnExit(PlayerStateMachine context)
        {
            context.PlayerMovement.EndDash();
        }
    }
}