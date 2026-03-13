using ScriptableObjects;
using UnityEngine;

namespace CharacterSystems.Player.Combat
{
    public class AttackComponent : MonoBehaviour
    {
        public AttackSO inPlaceCombo;
        public AttackSO moveCombo;
        
        private const string InPlaceComboHash = "_inPlaceCombo";

        [SerializeField]
        private float comboCounter;

        private float _lastComboEnd;
        private const float ComboCount = 3;

        [SerializeField]
        private float lastAttackPressed;
        [SerializeField] 
        private float comboInputWindow;
        private bool _isAttacking;
        private AttackSO _currentAttack;
        private PlayerAnimationController _playerAnimationController;

        public AttackSO CurrentAttack => _currentAttack;
        public bool IsAttacking => _isAttacking;


        public void Initialize(PlayerAnimationController playerAnimationController)
        {
            _playerAnimationController = playerAnimationController;
        }

        public void StartAttack()
        {
            _playerAnimationController.PlayAttackAnimation();
        }

        public bool UpdateAttack()
        {
            if (Time.time - _lastComboEnd > 0.5f && comboCounter <= ComboCount)
            {
                if (Time.time - lastAttackPressed > comboInputWindow)
                {
                    var animationClip = _playerAnimationController.GetStateInfo();
                    comboCounter++;
                    lastAttackPressed = Time.time;
                    var length = animationClip.normalizedTime;
                    _playerAnimationController.PlayAttackAnimation();
                    if (comboCounter <= ComboCount) return true;
                    comboCounter = 0;
                    return false;
                }
            }
            
            return true;
        }
        

    }
}
