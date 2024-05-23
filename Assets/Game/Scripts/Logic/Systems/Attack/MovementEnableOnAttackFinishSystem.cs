using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct MovementEnableOnAttackFinishSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackFinishedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var movementFlagsLookup = SystemAPI.GetComponentLookup<MovementFlags>();
            foreach (RefRO<AttackFinishedEvent> attackFinishedEvent in SystemAPI.Query<RefRO<AttackFinishedEvent>>())
            {
                if (!movementFlagsLookup.HasComponent(attackFinishedEvent.ValueRO.Source))
                {
                    continue;
                }

                movementFlagsLookup.GetRefRW(attackFinishedEvent.ValueRO.Source).ValueRW.CanMove = true;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}