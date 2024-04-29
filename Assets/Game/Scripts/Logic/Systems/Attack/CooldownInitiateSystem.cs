using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct CooldownInitiateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ComponentLookup<AttackStates> attackStateLookup = SystemAPI.GetComponentLookup<AttackStates>();
            foreach (RefRO<AttackStartedEvent> attackEvent in SystemAPI.Query<RefRO<AttackStartedEvent>>())
            {
                Entity source = attackEvent.ValueRO.Source;

                if (!attackStateLookup.HasComponent(source))
                {
                    continue;
                }

                RefRW<AttackStates> attackState = attackStateLookup.GetRefRW(source);
                attackState.ValueRW.IsAttacking = true;
                attackState.ValueRW.IsOnCooldown = true;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}