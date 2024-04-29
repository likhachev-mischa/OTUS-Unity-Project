using Game.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    [UpdateInGroup(typeof(MovementSystemGroup))]
    public partial struct RotationAngleSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAllRW<LocalTransform>()
                .WithAll<RotationDirectionAngle, RotationSpeed>().Build();

            var job = new RotationAngleJob() { DeltaTime = SystemAPI.Time.DeltaTime };
            state.Dependency = job.Schedule(state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct RotationAngleJob : IJobEntity
    {
        private static readonly float EPSILON = 0.1f;
        public float DeltaTime;

        private void Execute(ref LocalTransform transform, in RotationDirectionAngle angle, in RotationSpeed speed)
        {
            float3 normal = transform.Forward();
            float currentAngle = math.atan2(normal.z, normal.x);
            currentAngle -= math.PIHALF;
            float delta = currentAngle - angle.Destination;
            if (math.abs(delta) > math.PI2)
            {
                delta -= math.sign(delta) * math.PI2;
            }

            if (math.abs(delta) <= EPSILON)
            {
                return;
            }

            int rotationAngle = (int)math.sign(angle.Destination) == (int)math.sign(angle.Initial) ? -1 : 1;

            transform = transform.RotateY(rotationAngle * speed.Value * DeltaTime);
        }
    }
}