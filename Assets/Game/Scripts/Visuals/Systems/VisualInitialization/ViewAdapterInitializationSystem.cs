using Game.Components;
using Game.Visuals.Components;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(VisualProxyInitializationSystemGroup))]
    public partial class ViewAdapterInitializationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<VisualProxySpawnRequest>();
            RequireForUpdate<VisualTransform>();
        }

        protected override void OnUpdate()
        {
            var singleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = singleton.CreateCommandBuffer(World.Unmanaged);

            foreach ((_, VisualTransform visualProxy, Entity entity) in SystemAPI
                         .Query<RefRO<VisualProxySpawnRequest>, VisualTransform>()
                         .WithEntityAccess())
            {
                if (!visualProxy.Value.TryGetComponent(out PlayerViewAdapter adapter))
                {
                    continue;
                }

                adapter.Entity = entity;
                ecb.AddComponent(entity,
                    new ViewAdapterComponent()
                    {
                        Value = adapter
                    });
            }
        }
    }
}