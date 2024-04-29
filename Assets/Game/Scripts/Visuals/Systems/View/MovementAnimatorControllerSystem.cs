using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Visuals.Systems
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(AnimationSystemGroup))]
    public partial class MovementAnimatorControllerSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityQuery stateQuery =
                SystemAPI.QueryBuilder().WithAll<MovementState>().WithAll<VisualAnimator>().Build();

            Dependency.Complete();
            var moveStateSetJob = new AnimatorMoveStateSetJob();
            moveStateSetJob.Run(stateQuery);

            EntityQuery speedQuery = SystemAPI.QueryBuilder().WithAll<MoveSpeed>().WithAll<VisualAnimator>().Build();

            var speedSetJob = new AnimatorSpeedSetJob();
            speedSetJob.Run(speedQuery);

            EntityQuery directionQuery = SystemAPI.QueryBuilder().WithAll<MoveDirection, LocalTransform>()
                .WithAll<VisualAnimator>().Build();

            var directionSetJob = new AnimatorDirectionSetJob();
            directionSetJob.Run(directionQuery);
        }
    }

    public partial struct AnimatorMoveStateSetJob : IJobEntity
    {
        private void Execute(in MovementState state, VisualAnimator animator)
        {
            animator.Value.SetMovementState(state.Value);
        }
    }

    public partial struct AnimatorSpeedSetJob : IJobEntity
    {
        private void Execute(in MoveSpeed moveSpeed, VisualAnimator animator)
        {
            animator.Value.SetSpeed(moveSpeed.Value);
        }
    }

    public partial struct AnimatorDirectionSetJob : IJobEntity
    {
        private void Execute(in MoveDirection direction, in LocalTransform transform, VisualAnimator animator)
        {
            if (direction.Value.Equals(float3.zero))
            {
                animator.Value.SetDirection(0, 0);
                return;
            }

            var vector = new float2(direction.Value.x, direction.Value.z);
            float3 normal = transform.Forward();
            float normalAngle = math.atan2(normal.z, normal.x);
            float directionAngle = math.atan2(vector.y, vector.x) - normalAngle;
            directionAngle += math.PIHALF;
            var newDirection = new float2(math.cos(directionAngle), math.sin(directionAngle));
            animator.Value.SetDirection(newDirection.x, newDirection.y);
        }
    }
}