using Game.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Game.Systems
{
    [UpdateInGroup(typeof(MovementSystemGroup))]
    [UpdateBefore(typeof(MovementStateSetSystem))]
    public partial struct MovementFlagsSetSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<MovementDirection>().WithAllRW<MovementFlags>().Build();

            var job = new MovementFlagsSetJob();

            state.Dependency = job.ScheduleParallel(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct MovementFlagsSetJob : IJobEntity
    {
        private void Execute(in MovementDirection direction, ref MovementFlags flags)
        {
            if (!flags.CanMove || direction.Value.Equals(float3.zero))
            {
                flags.IsMoving = false;
                return;
            }

            flags.IsMoving = true;
        }
    }
}