using UnityEngine;

namespace MaykerStudio.VFX
{
    [ExecuteAlways]
    public class SlashDecal : MonoBehaviour
    {
        [SerializeField] private float decalOffsetY;

        [SerializeField] private float maxDistance = 0.5f;

        [SerializeField] private Vector3 rotation;

        [SerializeField] private ParticleSystem slash;

        public float capsuleHeight = 2f;
        public float capsuleRadius = 0.2f;

        private ParticleSystem decal;

        private CapsuleCollider slashCollider;

        private void Start()
        {
            decal = GetComponent<ParticleSystem>();
            decal.Stop(false);

            slashCollider = slash.GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            if (!slash || !decal || !slash.isPlaying)
                return;

            if (IsGrounded(out var hit))
            {
                transform.SetPositionAndRotation(hit + decalOffsetY * Vector3.up, Quaternion.Euler(rotation));
                if (!decal.isPlaying) decal.Play(true);
            }
            else if (decal.isPlaying)
            {
                decal.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }

        private void OnDrawGizmos()
        {
            if (!slash)
                return;

            if (IsGrounded(out var hit))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(slash.transform.position, Vector3.down * maxDistance);
                Gizmos.DrawWireSphere(hit, 0.1f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(slash.transform.position, Vector3.down * maxDistance);
            }

            Gizmos.color = Color.green;
            var position = slash.transform.position;
            var rotation = slash.transform.rotation;
            var up = rotation * Vector3.right;

            var point1 = position + up * (capsuleHeight / 2 - capsuleRadius);
            var point2 = position - up * (capsuleHeight / 2 - capsuleRadius);

            Gizmos.DrawWireSphere(point1, capsuleRadius);
            Gizmos.DrawWireSphere(point2, capsuleRadius);
            Gizmos.DrawLine(point1 + Vector3.right * capsuleRadius, point2 + Vector3.right * capsuleRadius);
            Gizmos.DrawLine(point1 - Vector3.right * capsuleRadius, point2 - Vector3.right * capsuleRadius);
            Gizmos.DrawLine(point1 + Vector3.forward * capsuleRadius, point2 + Vector3.forward * capsuleRadius);
            Gizmos.DrawLine(point1 - Vector3.forward * capsuleRadius, point2 - Vector3.forward * capsuleRadius);
        }

        private bool IsGrounded(out Vector3 collisionPoint)
        {
            collisionPoint = Vector3.zero;

            var rotation = slash.transform.rotation;
            var up = rotation * Vector3.right;
            var position = slash.transform.position;

            var point1 = position + up * (capsuleHeight / 2 - capsuleRadius);
            var point2 = position - up * (capsuleHeight / 2 - capsuleRadius);

            if (Physics.CapsuleCast(point1, point2, capsuleRadius, -up, out var hit, maxDistance))
            {
                collisionPoint = hit.point;
                return true;
            }

            if (Physics.CapsuleCast(point1, point2, capsuleRadius, up, out hit, maxDistance))
            {
                collisionPoint = hit.point;
                return true;
            }

            return false;
        }
    }
}