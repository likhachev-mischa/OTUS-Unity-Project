using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup))]
    public partial struct WeaponCollisionDataCleanupOnAttackFinishSystem : ISystem
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
            var collisionDataLookup = SystemAPI.GetComponentLookup<WeaponCollisionData>();

            foreach (Entity entity in array)
            {
                var source = eventLookup.GetRefRO(entity).ValueRO.Source;
                if (!weaponLookup.TryGetComponent(source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                if (!collisionDataLookup.HasComponent(weaponEntity.Value))
                {
                    continue;
                }

                collisionDataLookup.GetRefRW(weaponEntity.Value).ValueRW.CollidedEntities.Dispose();
            }

            array.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}