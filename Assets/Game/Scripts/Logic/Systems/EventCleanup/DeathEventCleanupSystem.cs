using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(EventCleanupSystemGroup))]
    public partial struct DeathEventCleanupSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<DeathEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder().WithAll<DeathEvent>().Build();
            state.EntityManager.DestroyEntity(query);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}