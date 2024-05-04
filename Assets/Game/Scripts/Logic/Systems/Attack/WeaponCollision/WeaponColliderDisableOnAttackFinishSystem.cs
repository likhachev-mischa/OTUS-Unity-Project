using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup))]
    public partial struct WeaponColliderDisableOnAttackFinishSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackFinishedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var query = SystemAPI.QueryBuilder().WithAll<AttackFinishedEvent>().Build();
            var array = query.ToEntityArray(Allocator.Temp);
            var eventLookup = SystemAPI.GetComponentLookup<AttackFinishedEvent>();
            var weaponLookup = SystemAPI.GetComponentLookup<WeaponEntity>();
            var colliderLookup = SystemAPI.GetComponentLookup<PhysicsCollider>();

            foreach (Entity entity in array)
            {
                var source = eventLookup.GetRefRO(entity).ValueRO.Source;
                if (!weaponLookup.TryGetComponent(source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                var collider = colliderLookup.GetRefRW(weaponEntity.Value);
                var cachedFilter = collider.ValueRO.Value.Value.GetCollisionFilter();
                collider.ValueRW.Value.Value.SetCollisionFilter(new CollisionFilter()
                    { BelongsTo = 0, CollidesWith = 0 });
                state.EntityManager.SetComponentData(weaponEntity.Value,
                    new CachedWeaponCollisionFilter()
                        { BelongsTo = cachedFilter.BelongsTo, CollidesWith = cachedFilter.CollidesWith });
            }


            array.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}