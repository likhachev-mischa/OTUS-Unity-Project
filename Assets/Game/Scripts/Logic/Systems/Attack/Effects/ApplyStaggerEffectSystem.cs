using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackEffectsSystemGroup))]
    public partial struct ApplyStaggerEffectSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var staggerSourceLookup = SystemAPI.GetComponentLookup<StaggerSource>();
            var staggeredLookup = SystemAPI.GetComponentLookup<Staggered>();

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (RefRO<AttackEvent> attackEvent in SystemAPI.Query<RefRO<AttackEvent>>())
            {
                if (!staggerSourceLookup.TryGetComponent(attackEvent.ValueRO.Source, out StaggerSource staggerSource)
                    || staggeredLookup.HasComponent(attackEvent.ValueRO.Target))
                {
                    continue;
                }

                Debug.Log("staggered");
                ecb.AddComponent<Staggered>(attackEvent.ValueRO.Target);
                ecb.SetComponent(attackEvent.ValueRO.Target, new Staggered() { Duration = staggerSource.Duration });
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