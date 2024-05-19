using SaveSystem.Components;
using Unity.Burst;
using Unity.Entities;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(SystemSaveLoadSystemGroup))]
    public partial struct SystemSaveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SystemSaveEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder().WithAll<SystemSaveEvent>().Build();
            state.EntityManager.DestroyEntity(query);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}