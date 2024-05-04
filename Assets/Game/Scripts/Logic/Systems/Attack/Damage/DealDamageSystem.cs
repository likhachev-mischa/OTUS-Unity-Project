using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(DamageCalculationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct DealDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var eventQuery = SystemAPI.QueryBuilder().WithAll<DealDamageEvent>().Build();
            state.EntityManager.DestroyEntity(eventQuery);

            var healthLookup = SystemAPI.GetComponentLookup<Health>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((RefRO<DealDamageRequest> dealDamageRequest, Entity request) in SystemAPI
                         .Query<RefRO<DealDamageRequest>>().WithEntityAccess())
            {
                ecb.DestroyEntity(request);

                var health = healthLookup.GetRefRW(dealDamageRequest.ValueRO.Target);
                health.ValueRW.Value = math.max(0, health.ValueRO.Value - dealDamageRequest.ValueRO.Value);

                var @event = ecb.CreateEntity();
                ecb.AddComponent(@event, new DealDamageEvent()
                {
                    Source = dealDamageRequest.ValueRO.Source, Target = dealDamageRequest.ValueRO.Target,
                    Value = dealDamageRequest.ValueRO.Value
                });
                Debug.Log("damage event sent");
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