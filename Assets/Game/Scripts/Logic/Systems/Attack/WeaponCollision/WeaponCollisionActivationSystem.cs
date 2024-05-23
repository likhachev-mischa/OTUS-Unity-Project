using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup))]
    public partial struct WeaponCollisionActivationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackStartedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            /*var query = SystemAPI.QueryBuilder().WithAll<AttackStartedEvent>().Build();
            var array = query.ToEntityArray(Allocator.Temp);
            var eventLookup = SystemAPI.GetComponentLookup<AttackStartedEvent>();
            var weaponLookup = SystemAPI.GetComponentLookup<WeaponEntity>();
            var colliderLookup = SystemAPI.GetComponentLookup<PhysicsCollider>();
            var cacheLookup = SystemAPI.GetComponentLookup<CachedWeaponCollisionFilter>();
            foreach (Entity entity in array)
            {
                var source = eventLookup.GetRefRO(entity).ValueRO.Source;
                if (!weaponLookup.TryGetComponent(source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                if (!cacheLookup.TryGetComponent(weaponEntity.Value, out CachedWeaponCollisionFilter cachedFilter) ||
                    !colliderLookup.HasComponent(weaponEntity.Value))
                {
                    continue;
                }

                var collider = colliderLookup.GetRefRW(weaponEntity.Value);
                collider.ValueRW.Value.Value.SetCollisionFilter(new CollisionFilter()
                    { CollidesWith = cachedFilter.CollidesWith, BelongsTo = cachedFilter.BelongsTo });
            }


            array.Dispose();*/
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}