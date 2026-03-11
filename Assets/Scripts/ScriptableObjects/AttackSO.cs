using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable/Combat/Attack")]
    public class AttackSO : ScriptableObject
    {
        public AnimatorOverrideController animatorOverrideController;

        public float damage;
        public float knockback;

        public float forwardMovement;

        public AttackSO nextAttack;
    }
}
