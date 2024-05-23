using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct MovementDisableOnAttackSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackStartedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var movementFlagsLookup = SystemAPI.GetComponentLookup<MovementFlags>();

            foreach (RefRO<AttackStartedEvent> attackStartedEvent in SystemAPI.Query<RefRO<AttackStartedEvent>>())
            {
                if (!movementFlagsLookup.HasComponent(attackStartedEvent.ValueRO.Source))
                {
                    continue;
                }

                movementFlagsLookup.GetRefRW(attackStartedEvent.ValueRO.Source).ValueRW.CanMove = false;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}