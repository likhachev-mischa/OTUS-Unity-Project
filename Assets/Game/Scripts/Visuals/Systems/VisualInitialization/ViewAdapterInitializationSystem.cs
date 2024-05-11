using Game.Components;
using Game.Visuals.Components;
using Unity.Collections;
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
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);

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

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}