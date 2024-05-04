using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup), OrderLast = true)]
    public partial struct WeaponCollisionEventCleanupSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackCollisionEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder().WithAll<AttackCollisionEvent>().Build();
            state.Dependency.Complete();
            state.EntityManager.DestroyEntity(query);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}