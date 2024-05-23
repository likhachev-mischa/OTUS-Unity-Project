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
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<MovementState>().WithAll<MovementSpeedShared>()
                .WithAll<SpeedUpCurve>().WithAllRW<MovementSpeed>().Build();

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

        private void Execute(ref MovementSpeed movementSpeed, ref SpeedUpCurve curve,in MovementSpeedShared movementSpeedShared,
            in MovementState state)
        {
            if (state.Value == MovementStates.IDLE)
            {
                curve.ElapsedTime = 0f;
                movementSpeed.Value = 0f;
                return;
            }

            curve.ElapsedTime += deltaTime;

            float delta = curve.ElapsedTime / curve.SpeedUpTime;
            delta = math.min(delta, 1f);
            int steps = curve.Curve.Value.Array.Length - 1;

            var currentStep = (int)(delta * steps);

            movementSpeed.Value = curve.Curve.Value.Array[currentStep] * movementSpeedShared.Value;
        }
    }
}