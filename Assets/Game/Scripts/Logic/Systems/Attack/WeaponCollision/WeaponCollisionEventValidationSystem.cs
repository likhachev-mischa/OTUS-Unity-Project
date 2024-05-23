using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup), OrderFirst = true)]
    public partial struct WeaponCollisionEventValidationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var collisionDataLookup = SystemAPI.GetComponentLookup<WeaponCollisionData>();
            var inactiveLookup = SystemAPI.GetComponentLookup<Inactive>();
            foreach (var (@event, eventEntity) in SystemAPI.Query<RefRO<AttackEvent>>().WithEntityAccess())
            {
                if (!collisionDataLookup.HasComponent(@event.ValueRO.Source))
                {
                    continue;
                }

                if (inactiveLookup.HasComponent(@event.ValueRO.Source))
                {
                    ecb.DestroyEntity(eventEntity);
                    continue;
                }

                var list = collisionDataLookup.GetRefRW(@event.ValueRO.Source).ValueRW.CollidedEntities;

                if (list.Contains(@event.ValueRO.Target))
                {
                    ecb.DestroyEntity(eventEntity);
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