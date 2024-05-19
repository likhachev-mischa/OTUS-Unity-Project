using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(AnimationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class ViewAdapterHealthDrawSystem : SystemBase
    {
        private EntityQuery query;

        protected override void OnCreate()
        {
            query = SystemAPI.QueryBuilder().WithAll<Health>()
                .WithAll<ViewAdapterComponent>().Build();
            query.AddChangedVersionFilter(typeof(Health));
        }

        protected override void OnUpdate()
        {
            var job = new DrawJob();
            Dependency.Complete();
            job.Run(query);
        }

        private partial struct DrawJob : IJobEntity
        {
            private void Execute(in Health health, ViewAdapterComponent viewAdapter)
            {
                viewAdapter.Value.DrawVisuals(ComponentToDraw.HEALTH, health.Value.ToString());
            }
        }
    }
}