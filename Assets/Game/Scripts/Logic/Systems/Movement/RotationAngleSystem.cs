using Game.Components;
using Unity.Burst;
using Unity.Collections;
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
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var eventQuery = SystemAPI.QueryBuilder().WithAll<RotationFinishedEvent>().Build();
            state.EntityManager.DestroyEntity(eventQuery);

            EntityQuery query = SystemAPI.QueryBuilder().WithAllRW<LocalTransform>()
                .WithAll<RotationDirectionAngle, RotationSpeed>().WithNone<Inactive>().Build();

            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            var job = new RotationAngleJob() { DeltaTime = SystemAPI.Time.DeltaTime, ECB = ecb };
            state.Dependency.Complete();
            job.Run(query);
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
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
        public EntityCommandBuffer ECB;
        public float DeltaTime;

        private void Execute(ref LocalTransform transform, in RotationDirectionAngle angle, in RotationSpeed speed,
            in Entity entity)
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
                var @event = ECB.CreateEntity();
                ECB.AddComponent(@event, new RotationFinishedEvent() { Source = entity });
                return;
            }

            int rotationAngle = -(int)math.sign(angle.Destination);

            transform = transform.RotateY(rotationAngle * speed.Value * DeltaTime);
        }
    }
}