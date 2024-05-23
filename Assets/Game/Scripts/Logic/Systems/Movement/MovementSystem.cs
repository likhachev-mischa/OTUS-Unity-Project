using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Game.Systems
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(MovementSystemGroup))]
    public partial struct MovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAllRW<LocalTransform>()
                .WithAll<MovementSpeed, MovementDirection, MovementFlags>()
                .Build();
            float deltaTime = SystemAPI.Time.DeltaTime;
            var job = new TransformMoveJob() { deltaTime = deltaTime };

            state.Dependency = job.ScheduleParallel(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct TransformMoveJob : IJobEntity
    {
        [ReadOnly]
        public float deltaTime;

        private void Execute(ref LocalTransform transform, in MovementSpeed movementSpeed,
            in MovementDirection direction, in MovementFlags movementFlags)
        {
            if (movementFlags.IsMoving)
            {
                transform = transform.Translate(direction.Value * movementSpeed.Value * deltaTime);
            }
        }
    }
}