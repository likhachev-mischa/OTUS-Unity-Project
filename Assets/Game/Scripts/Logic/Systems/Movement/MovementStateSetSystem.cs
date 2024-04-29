using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(MovementSystemGroup))]
    public partial struct MovementStateSetSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAllRW<MovementState>().WithAll<MovementFlags>().Build();

            var job = new MovementStateSetJob();

            state.Dependency = job.Schedule(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct MovementStateSetJob : IJobEntity
    {
        private void Execute(ref MovementState state, in MovementFlags flags)
        {
            if (!flags.CanMove || !flags.IsMoving)
            {
                state.Value = MovementStates.IDLE;
                return;
            }

            state.Value = MovementStates.MOVING;
        }
    }
}