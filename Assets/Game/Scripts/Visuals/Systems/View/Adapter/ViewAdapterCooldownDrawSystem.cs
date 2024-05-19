using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(AnimationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class ViewAdapterCooldownDrawSystem : SystemBase
    {
        private EntityQuery query;

        protected override void OnCreate()
        {
            query = SystemAPI.QueryBuilder().WithAll<AttackCooldown>().WithAll<AttackStates>()
                .WithAll<ViewAdapterComponent>().Build();
            query.AddChangedVersionFilter(typeof(AttackCooldown));
        }

        protected override void OnUpdate()
        {
            var job = new DrawJob();
            Dependency.Complete();
            job.Run(query);
        }

        private partial struct DrawJob : IJobEntity
        {
            private void Execute(in AttackCooldown attackCooldown, in AttackStates attackStates,
                ViewAdapterComponent viewAdapter)
            {
                if (attackStates.IsOnCooldown)
                {
                    viewAdapter.Value.DrawVisuals(ComponentToDraw.ATTACK_COOLDOWN,
                        attackCooldown.Value.ToString());
                }
                else
                {
                    viewAdapter.Value.DisableVisuals(ComponentToDraw.ATTACK_COOLDOWN);
                }
            }
        }
    }
}