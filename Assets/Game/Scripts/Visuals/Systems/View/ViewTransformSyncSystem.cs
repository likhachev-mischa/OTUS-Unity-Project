using Game.Components;
using Game.Systems;
using Unity.Entities;
using Unity.Transforms;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(AnimationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class ViewTransformSyncSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAllRW<VisualTransform>().WithAll<LocalTransform>()
                .Build();

            var job = new TransformSyncJob();

            Dependency.Complete();
            job.Run(query);
        }
    }

    public partial struct TransformSyncJob : IJobEntity
    {
        private void Execute(VisualTransform visualTransform, in LocalTransform localTransform)
        {
            visualTransform.Value.position = localTransform.Position;
            visualTransform.Value.rotation = localTransform.Rotation;
        }
    }
}