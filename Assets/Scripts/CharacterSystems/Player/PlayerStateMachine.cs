using CharacterSystems.Player.Combat;
using CharacterSystems.Player.PlayerStates;
using Interface;
using State;
using UnityEngine;

namespace CharacterSystems.Player
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private StateMachine<PlayerStateMachine> _stateMachine;

        [SerializeField] string _currentStateName;

        //States
        public PlayerAttackState PlayerAttackState { get; private set; }
        public PlayerMoveState PlayerMoveState { get; private set; }
        public PlayerIdleState PlayerIdleState { get; private set; }
        public PlayerDashState PlayerDashState { get; private set; }


        //Components
        public PlayerMovement PlayerMovement { get; private set; }
        public PlayerInputHandler PlayerInputHandler { get; private set; }
        public PlayerAnimationController PlayerAnimationController { get; private set; }
        public AttackComponent PlayerAttackComponent { get; private set; }

        protected void Awake()
        {
            _stateMachine = new StateMachine<PlayerStateMachine>(this);

            PlayerAttackState = new PlayerAttackState();
            PlayerMoveState = new PlayerMoveState();
            PlayerIdleState = new PlayerIdleState();
            PlayerDashState = new PlayerDashState();

            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerInputHandler = GetComponent<PlayerInputHandler>();
            PlayerAnimationController = GetComponent<PlayerAnimationController>();
            PlayerAttackComponent = GetComponent<AttackComponent>();
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
            _currentStateName = newState.GetType().Name;
        }
    }
}