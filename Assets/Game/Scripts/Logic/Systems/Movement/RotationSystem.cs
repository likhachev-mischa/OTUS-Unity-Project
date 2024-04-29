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
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAllRW<LocalTransform>()
                .WithAll<RotationDirection, RotationSpeed>()
                .Build();
            float deltaTime = SystemAPI.Time.DeltaTime;

            var job = new RotationJob() { deltaTime = deltaTime };
            state.Dependency = job.Schedule(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct RotationJob : IJobEntity
    {
        private static readonly float EPSILON = 0.1f;

        [ReadOnly]
        public float deltaTime;

        private void Execute(ref LocalTransform transform, in RotationDirection direction, in RotationSpeed speed)
        {
            float3 normal = transform.Forward();
            float currentAngle = math.atan2(normal.z, normal.x);

            float angle = currentAngle -
                          math.atan2(direction.Value.z, direction.Value.x);
            var sign = (int)math.sign(angle);
            float oppositeAngle = math.PI2 + -sign * angle;

            float abs = math.abs(angle);
            if (math.abs(oppositeAngle) < abs)
            {
                angle = -sign;
            }
            else if (abs < EPSILON)
            {
                angle = 0;
            }
            else
            {
                angle = sign;
            }

            transform = transform.RotateY(angle * speed.Value * deltaTime);
        }
    }
}