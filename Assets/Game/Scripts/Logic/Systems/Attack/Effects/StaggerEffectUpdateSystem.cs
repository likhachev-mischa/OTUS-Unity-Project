using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackEffectsSystemGroup))]
    public partial struct StaggerEffectUpdateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Staggered>();
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var movementFlagsLookup = SystemAPI.GetComponentLookup<MovementFlags>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((RefRW<Staggered> staggered, Entity entity) in SystemAPI.Query<RefRW<Staggered>>()
                         .WithEntityAccess())
            {
                staggered.ValueRW.Duration -= deltaTime;
                if (staggered.ValueRO.Duration <= 0)
                {
                    ecb.RemoveComponent<Staggered>(entity);

                    if (movementFlagsLookup.HasComponent(entity))
                    {
                        movementFlagsLookup.GetRefRW(entity).ValueRW.CanMove = true;
                    }

                    continue;
                }

                if (movementFlagsLookup.HasComponent(entity))
                {
                    movementFlagsLookup.GetRefRW(entity).ValueRW.CanMove = false;
                }
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}