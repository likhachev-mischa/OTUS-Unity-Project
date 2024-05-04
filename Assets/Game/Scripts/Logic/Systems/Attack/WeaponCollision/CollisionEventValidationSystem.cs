using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup), OrderFirst = true)]
    public partial struct CollisionEventValidationSystem : ISystem
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
            var array = query.ToEntityArray(Allocator.Temp);

            var eventLookup = SystemAPI.GetComponentLookup<AttackCollisionEvent>();
            var collisionDataLookup = SystemAPI.GetComponentLookup<WeaponCollisionData>();
            foreach (Entity entity in array)
            {
                var @event = eventLookup.GetRefRO(entity).ValueRO;
                if (!collisionDataLookup.HasComponent(@event.Source))
                {
                    continue;
                }

                var list = collisionDataLookup.GetRefRW(@event.Source).ValueRW.CollidedEntities;
                if (list.BinarySearch(@event.Target) >= 0)
                {
                    state.EntityManager.DestroyEntity(entity);
                    continue;
                }

                list.Add(@event.Target);
            }

            array.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}