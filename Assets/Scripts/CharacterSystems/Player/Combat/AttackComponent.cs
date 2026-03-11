using ScriptableObjects;
using UnityEngine;

namespace CharacterSystems.Player.Combat
{
    public class AttackComponent : MonoBehaviour
    {
        public AttackSO inPlaceCombo;
        public AttackSO moveCombo;
        private bool _comboQueued;

        private AttackSO _currentAttack;

        private PlayerAnimationController _playerAnimationController;

        public void StartAttack(bool isMoving, PlayerAnimationController playerAnimationController)
        {
            _currentAttack = isMoving ? moveCombo : inPlaceCombo;

            _playerAnimationController = playerAnimationController;

            PlayAttack(playerAnimationController);
        }

        public void QueueNextAttack()
        {
            _comboQueued = true;
        }

// AttackComponent.cs
        public bool OnAttackFinished()
        {
            if (_comboQueued && _currentAttack.nextAttack != null)
            {
                _currentAttack = _currentAttack.nextAttack;
                _comboQueued = false;
                PlayAttack(_playerAnimationController);
                _playerAnimationController.PlayAttack();
                return true; // combo continued
            }
            
            _comboQueued = false;
            return false; // no combo, attack ended
        }

        private void PlayAttack(PlayerAnimationController playerAnimationController)
        {
            playerAnimationController.OverrideController(_currentAttack.animatorOverrideController);
        }
    }
}