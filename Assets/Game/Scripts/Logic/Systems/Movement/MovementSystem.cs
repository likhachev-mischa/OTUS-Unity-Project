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
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAllRW<LocalTransform>().WithAll<MoveSpeed, MoveDirection>()
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

        private void Execute(ref LocalTransform transform, in MoveSpeed moveSpeed, in MoveDirection direction)
        {
            transform = transform.Translate(direction.Value * moveSpeed.Value * deltaTime);
        }
    }
}