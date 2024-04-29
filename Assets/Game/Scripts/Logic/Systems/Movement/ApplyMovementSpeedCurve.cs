using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Game.Systems
{
    [UpdateInGroup(typeof(MovementSystemGroup))]
    [UpdateBefore(typeof(MovementSystem))]
    public partial struct ApplyMovementSpeedCurve : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<MovementState>()
                .WithAll<SpeedUpCurve>().WithAllRW<MoveSpeed>().Build();

            var job = new SpeedCurveJob() { deltaTime = SystemAPI.Time.DeltaTime };

            state.Dependency = job.Schedule(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct SpeedCurveJob : IJobEntity
    {
        [ReadOnly]
        public float deltaTime;

        private void Execute(ref MoveSpeed moveSpeed, ref SpeedUpCurve curve,
            in MovementState state)
        {
            if (state.Value == MovementStates.IDLE)
            {
                curve.ElapsedTime = 0f;
                moveSpeed.Value = 0f;
                return;
            }

            curve.ElapsedTime += deltaTime;

            float delta = curve.ElapsedTime / curve.SpeedUpTime.Value;
            delta = math.min(delta, 1f);
            int steps = curve.Curve.Value.Array.Length - 1;

            var currentStep = (int)(delta * steps);

            moveSpeed.Value = curve.Curve.Value.Array[currentStep] * moveSpeed.InitialValue.Value;
        }
    }
}