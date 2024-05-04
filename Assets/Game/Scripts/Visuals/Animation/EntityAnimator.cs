using Game.Components;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class EntityAnimator : MonoBehaviour
    {
        private static readonly int MovementId = Animator.StringToHash("MainState");
        private static readonly int SpeedId = Animator.StringToHash("SpeedMultiplier");

        private static readonly int DirectionZ = Animator.StringToHash("DirectionZ");
        private static readonly int DirectionX = Animator.StringToHash("DirectionX");

        private static readonly int AttackId = Animator.StringToHash("Attack");
        private static readonly int TakeDamageId = Animator.StringToHash("Take Damage");

        [SerializeField]
        private float initialAnimationSpeed;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float lerpFactor = 0.04f;

        private MovementStates currentMovementState;

        public void SetMovementState(MovementStates state)
        {
            if (state == currentMovementState)
            {
                return;
            }

            currentMovementState = state;
            animator.SetInteger(MovementId, (int)state);
        }

        public void SetSpeed(float speed)
        {
            float speedMult = speed / initialAnimationSpeed;
            animator.SetFloat(SpeedId, speedMult);
        }

        public void SetDirection(float x, float z)
        {
            float oldX = animator.GetFloat(DirectionX);
            float newX = math.lerp(oldX, x, lerpFactor);
            float oldZ = animator.GetFloat(DirectionZ);
            float newZ = math.lerp(oldZ, z, lerpFactor);
            animator.SetFloat(DirectionX, newX);
            animator.SetFloat(DirectionZ, newZ);
        }

        public void SetAttackTrigger()
        {
            animator.SetTrigger(AttackId);
        }

        public void SetTakeDamageTrigger()
        {
            animator.SetTrigger(TakeDamageId);
        }
    }
}