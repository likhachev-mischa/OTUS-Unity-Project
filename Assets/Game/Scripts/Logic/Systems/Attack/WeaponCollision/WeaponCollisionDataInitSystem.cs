using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup))]
    public partial struct WeaponCollisionDataInitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackStartedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var weaponLookup = SystemAPI.GetComponentLookup<WeaponEntity>();
            var collisionDataLookup = SystemAPI.GetComponentLookup<WeaponCollisionData>();
            foreach (var @event in SystemAPI.Query<RefRO<AttackStartedEvent>>())
            {
                Entity source = @event.ValueRO.Source;
                if (!weaponLookup.TryGetComponent(source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                if (!collisionDataLookup.HasComponent(weaponEntity.Value))
                {
                    continue;
                }

                var collisionData = collisionDataLookup.GetRefRW(weaponEntity.Value);
                collisionData.ValueRW.CollidedEntities = new NativeList<Entity>(10, Allocator.Persistent);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}