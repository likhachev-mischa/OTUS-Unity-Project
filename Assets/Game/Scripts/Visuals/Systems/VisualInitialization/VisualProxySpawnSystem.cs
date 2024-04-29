using Game.Components;
using Game.Visuals.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Visuals.Systems
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(VisualProxyInitializationSystemGroup), OrderFirst = true)]
    public partial class VisualProxySpawnSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var singleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = singleton.CreateCommandBuffer(World.Unmanaged);
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<VisualProxySpawnRequest>().WithAll<VisualProxyPrefab>()
                .Build();

            NativeArray<Entity> entityArray = query.ToEntityArray(Allocator.Temp);

            foreach (Entity entity in entityArray)
            {
                var prefab = EntityManager.GetComponentData<VisualProxyPrefab>(entity);
                GameObject go = Object.Instantiate(prefab.Value);

                Transform transform = go.transform;
                EntityManager.AddComponentObject(entity, new VisualTransform() { Value = transform });
                ecb.RemoveComponent<VisualProxySpawnRequest>(entity);
            }

            entityArray.Dispose();
        }
    }
}