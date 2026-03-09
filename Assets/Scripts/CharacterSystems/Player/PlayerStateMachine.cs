using System;
using CharacterSystems.Player.PlayerStates;
using Interface;
using State;
using Unity.VisualScripting;
using UnityEngine;

namespace CharacterSystems.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private StateMachine<PlayerStateMachine> _stateMachine;
        
        //States
        public PlayerAttackState PlayerAttackState { get; private set; } 
        public PlayerMoveState PlayerMoveState { get; private set; }
        public PlayerIdleState PlayerIdleState { get; private set; }
        
        
        //Components
        public PlayerMovement PlayerMovement { get; private set; }
        public PlayerInputHandler PlayerInputHandler { get; private set; }

        protected void Awake()
        {
            _stateMachine = new StateMachine<PlayerStateMachine>(this);

            PlayerAttackState = new PlayerAttackState();
            PlayerMoveState = new PlayerMoveState();
            PlayerIdleState = new PlayerIdleState();
            
            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerInputHandler = GetComponent<PlayerInputHandler>();
        }

        protected void Start()
        {
            ChangeState(PlayerIdleState);
        }

        protected void Update()
        {
            _stateMachine?.Update();
        }

        public void ChangeState(IState<PlayerStateMachine> newState)
        {
            _stateMachine.ChangeState(newState);
        }
    }
}