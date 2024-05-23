using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(MovementSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct EnemyRotationDirectionSetSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRO<MovementDirection> movementDirection,
                         RefRW<RotationDirection> rotationDirection,_) in SystemAPI
                         .Query<RefRO<MovementDirection>, RefRW<RotationDirection>,RefRO<EnemyTag>>())
            {
                rotationDirection.ValueRW.Value = movementDirection.ValueRO.Value;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}