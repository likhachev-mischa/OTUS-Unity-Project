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
                Debug.Log("step in");
                // Component weaponVisual = visualTransform.Value.GetComponentInChildren(typeof(WeaponViewAdapter));
                // if (weaponVisual == null)
                // {
                //     continue;
                // }
                if (viewAdapter.Value is not EntityViewAdapter entityViewAdapter)
                {
                    continue;
                }

                IViewAdapter weaponAdapter = entityViewAdapter.WeaponViewAdapter;
                weaponAdapter.Entity = weaponEntity.ValueRO.Value;
                ecb.AddComponent(weaponEntity.ValueRO.Value,
                    new ViewAdapterComponent() { Value = weaponAdapter });

                if (EntityManager.HasComponent<VisualProxyPrefab>(weaponEntity.ValueRO.Value))
                {
                    Debug.Log("no component");
                    pool.Value.GetObject(EntityManager.GetComponentData<VisualProxyPrefab>(weaponEntity.ValueRO.Value)
                        .Value, entityViewAdapter.WeaponViewAdapter.transform);
                }
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}