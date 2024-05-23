using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(WeaponInitializationSystemGroup))]
    public partial class VisualWeaponInitializationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<ObjectPoolComponent>();
            RequireForUpdate<WeaponInitializationRequest>();
        }

        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var poolQuery = SystemAPI.QueryBuilder().WithAll<ObjectPoolComponent>().Build();
            var pool = poolQuery.GetSingleton<ObjectPoolComponent>();

            foreach ((_, RefRO<WeaponEntity> weaponEntity,
                         var viewAdapter) in SystemAPI
                         .Query<RefRO<WeaponInitializationRequest>, RefRO<WeaponEntity>, ViewAdapterComponent>())
            {
                if (viewAdapter.Value is not IWeaponViewAdapterContainer weaponAdapterContainer)
                {
                    continue;
                }

                IViewAdapter weaponAdapter = weaponAdapterContainer.WeaponViewAdapter;
                weaponAdapter.Entity = weaponEntity.ValueRO.Value;
                ecb.AddComponent(weaponEntity.ValueRO.Value,
                    new ViewAdapterComponent() { Value = weaponAdapter });

                if (EntityManager.HasComponent<VisualProxyPrefab>(weaponEntity.ValueRO.Value))
                {
                    var transform = weaponAdapterContainer.WeaponViewAdapter.transform;
                    pool.Value.SpawnObject(EntityManager.GetComponentData<VisualProxyPrefab>(weaponEntity.ValueRO.Value)
                        .Value, transform.position, transform.rotation, transform);
                }
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}