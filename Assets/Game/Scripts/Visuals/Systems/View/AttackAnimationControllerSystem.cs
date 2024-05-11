using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(AnimationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class AttackAnimationControllerSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<AttackStartedEvent>();
        }

        protected override void OnUpdate()
        {
            foreach (RefRO<AttackStartedEvent> attackStartedEvent in SystemAPI.Query<RefRO<AttackStartedEvent>>())
            {
                Entity source = attackStartedEvent.ValueRO.Source;
                if (!EntityManager.HasComponent<VisualAnimator>(source))
                {
                    continue;
                }

                EntityAnimator animator = EntityManager.GetComponentObject<VisualAnimator>(source).Value;
                animator.SetAttackTrigger();
            }
        }
    }
}