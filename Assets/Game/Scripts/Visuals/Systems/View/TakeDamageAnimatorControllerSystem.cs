using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(AnimationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class TakeDamageAnimatorControllerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (RefRO<DealDamageEvent> dealDamageEvent in SystemAPI.Query<RefRO<DealDamageEvent>>())
            {
                Entity target = dealDamageEvent.ValueRO.Target;
                if (!EntityManager.HasComponent<VisualAnimator>(target))
                {
                    continue;
                }

                EntityAnimator animator = EntityManager.GetComponentObject<VisualAnimator>(target).Value;
                animator.SetTakeDamageTrigger();
            }
        }
    }
}