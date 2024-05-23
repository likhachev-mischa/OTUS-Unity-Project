using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Game.Systems
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(MovementSystemGroup), OrderFirst = true)]
    public partial struct MovementDirectionSetFromControllerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MoveDirectionController>();
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var controllerDirection = SystemAPI.GetSingleton<MoveDirectionController>();
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<ControllableTag, RotationDirection>()
                .WithAllRW<MovementDirection>()
                .Build();

            var job = new MoveDirectionSetJob() { controllerDirection = controllerDirection.Value };
            state.Dependency = job.Schedule(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct MoveDirectionSetJob : IJobEntity
    {
        [ReadOnly]
        public float2 controllerDirection;

        private void Execute(ref MovementDirection movementDirection, in RotationDirection rotationDirection)
        {
            if (controllerDirection.Equals(float2.zero))
            {
                movementDirection.Value = float3.zero;
                return;
            }

            float3 normal = rotationDirection.Value;

            float angle = math.atan2(normal.z, normal.x);
            float controllerAngle = math.atan2(controllerDirection.y, controllerDirection.x);

            controllerAngle += angle;
            controllerAngle -= math.PIHALF;

            var moveDir = new float2(math.cos(controllerAngle), math.sin(controllerAngle));

            movementDirection.Value.x = moveDir.x;
            movementDirection.Value.z = moveDir.y;
        }
    }
}