using Game.Authoring;
using Game.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Visuals.Systems
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(VisualProxyInitializationSystemGroup), OrderFirst = true)]
    public partial class VisualProxySpawnSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<ObjectPoolComponent>();
        }

        protected override void OnUpdate()
        {
            var singleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            var poolQuery = SystemAPI.QueryBuilder().WithAll<ObjectPoolComponent>().Build();
            var pool = poolQuery.GetSingleton<ObjectPoolComponent>().Value;

            EntityCommandBuffer ecb = singleton.CreateCommandBuffer(World.Unmanaged);
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<VisualProxySpawnRequest>().WithAll<VisualProxyPrefab>()
                .Build();

            NativeArray<Entity> entityArray = query.ToEntityArray(Allocator.Temp);

            foreach (Entity entity in entityArray)
            {
                var prefab = EntityManager.GetComponentData<VisualProxyPrefab>(entity);
                var go = pool.SpawnObject(prefab.Value).gameObject;

                Transform transform = go.transform;
                EntityManager.AddComponentObject(entity, new VisualTransform() { Value = transform });
                ecb.RemoveComponent<VisualProxySpawnRequest>(entity);
            }

            entityArray.Dispose();
        }
    }
}