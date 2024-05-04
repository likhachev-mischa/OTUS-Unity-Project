using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(DamageCalculationSystemGroup), OrderLast = true)]
    [RequireMatchingQueriesForUpdate]
    public partial struct DamageEventCleanupSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // var requestQuery = SystemAPI.QueryBuilder().WithAll<DealDamageRequest>().Build();
            // requestQuery.CompleteDependency();
            // state.EntityManager.DestroyEntity(requestQuery);
            //
            // var eventQuery = SystemAPI.QueryBuilder().WithAll<DealDamageEvent>().Build();
            // eventQuery.CompleteDependency();
            // state.EntityManager.DestroyEntity(eventQuery);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}