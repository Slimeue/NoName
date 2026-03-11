using UnityEngine;

namespace MaykerStudio.Demo
{
    public class ParticleAnimationEvent : MonoBehaviour
    {
        [SerializeField] private Transform particleContainer;

        public void PlayParticle()
        {
            var particleSystem = GetComponentInChildren<ParticleSystem>();

            if (particleSystem && !particleSystem.GetComponent<Projectile>())
                particleSystem.Play(true);
            else
                AttackerController.Instance.SpawnProjectile();
        }
    }
}