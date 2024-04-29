using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(MovementSystemGroup))]
    [UpdateBefore(typeof(RotationSystem))]
    public partial struct RotationDirectionSetFromControllerSystem : ISystem
    {
        private float2 cachedMousePosition;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MousePositionController>();
            cachedMousePosition = float2.zero;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var mousePosition = SystemAPI.GetSingleton<MousePositionController>();

            if (mousePosition.Value.Equals(cachedMousePosition))
            {
                return;
            }

            cachedMousePosition = mousePosition.Value;

            EntityQuery query = SystemAPI.QueryBuilder().WithAll<ControllableTag, LocalTransform>()
                .WithAllRW<RotationDirection>().Build();

            var job = new RotationDirectionSetJob() { mousePosition = mousePosition.Value };
            state.Dependency = job.Schedule(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct RotationDirectionSetJob : IJobEntity
    {
        [ReadOnly]
        public float2 mousePosition;

        private void Execute(ref RotationDirection rotationDirection, in LocalTransform transform)
        {
            var vector = new float2(mousePosition.x - transform.Position.x, mousePosition.y - transform.Position.z);
            vector = math.normalize(vector);

            rotationDirection.Value.x = vector.x;
            rotationDirection.Value.z = vector.y;
        }
    }
}