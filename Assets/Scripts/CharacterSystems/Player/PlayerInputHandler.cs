using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterSystems.Player
{
    public class PlayerInputHandler : MonoBehaviour, InputSystem_Actions.IPlayerActions
    {
        private InputSystem_Actions _actions;

        public Vector2 MoveInput { get; private set; }
        public bool AttackPressed { get; private set; }
        public bool DashPressed { get; private set; }

        private void OnEnable()
        {
            if (_actions == null)
            {
                _actions = new InputSystem_Actions();
                _actions.Player.SetCallbacks(this);
            }

            _actions.Player.Enable();
        }

        private void OnDisable()
        {
            _actions.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            AttackPressed = context.performed;
        }

        public void OnLook(InputAction.CallbackContext context) { }
        public void OnInteract(InputAction.CallbackContext context) { }
        public void OnCrouch(InputAction.CallbackContext context) { }

        public void OnDash(InputAction.CallbackContext context)
        {
            DashPressed = context.performed;
        }
        public void OnPrevious(InputAction.CallbackContext context) { }
        public void OnNext(InputAction.CallbackContext context) { }
        public void OnSprint(InputAction.CallbackContext context) { }
    }
}
