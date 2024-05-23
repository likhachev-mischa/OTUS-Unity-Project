using Game.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(MovementSystemGroup))]
    public partial struct EnemyMoveDirectionUpdateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackTarget>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach ((RefRO<AttackTarget> attackTarget, RefRW<MovementDirection> movementDirection,
                         var localTransform) in SystemAPI
                         .Query<RefRO<AttackTarget>, RefRW<MovementDirection>, RefRO<LocalTransform>>())
            {
                movementDirection.ValueRW.Value =
                    math.normalize(attackTarget.ValueRO.Position - localTransform.ValueRO.Position);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}