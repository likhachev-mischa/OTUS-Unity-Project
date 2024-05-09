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
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var collisionDataLookup = SystemAPI.GetComponentLookup<WeaponCollisionData>();
            foreach (var (@event, entity) in SystemAPI.Query<RefRO<AttackCollisionEvent>>().WithEntityAccess())
            {
                if (!collisionDataLookup.HasComponent(@event.ValueRO.Source))
                {
                    continue;
                }

                var list = collisionDataLookup.GetRefRW(@event.ValueRO.Source).ValueRW.CollidedEntities;
                if (list.BinarySearch(@event.ValueRO.Target) >= 0)
                {
                    ecb.DestroyEntity(entity);
                    continue;
                }

                list.Add(@event.ValueRO.Target);
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