using Game.Components;
using Game.Visuals.Components;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(VisualProxyInitializationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public sealed partial class VisualAnimatorInitializationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var singleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = singleton.CreateCommandBuffer(World.Unmanaged);
            foreach ((_, VisualTransform visualObject, Entity entity) in SystemAPI
                         .Query<RefRO<VisualProxySpawnRequest>,
                             VisualTransform>().WithEntityAccess())
            {
                if (!visualObject.Value.TryGetComponent<EntityAnimator>(out EntityAnimator animator))
                {
                    continue;
                }

                ecb.AddComponent(entity, new VisualAnimator() { Value = animator });
            }
        }
    }
}