using Game.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AISystemGroup))]
    public partial struct EnemyTargetUpdateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackTarget>();
            state.RequireForUpdate<CharacterTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            //character is singleton for now
            var characterEntity = SystemAPI.GetSingletonEntity<CharacterTag>();
            state.Dependency.Complete();
            float3 characterPosition = SystemAPI.GetComponentLookup<LocalTransform>().GetRefRO(characterEntity).ValueRO
                .Position;

            foreach ((RefRW<AttackTarget> attackTarget, RefRO<LocalTransform> localTransform) in SystemAPI
                         .Query<RefRW<AttackTarget>, RefRO<LocalTransform>>()) 
            {
                float3 entityPosition = localTransform.ValueRO.Position;
                attackTarget.ValueRW.Position = characterPosition;
                attackTarget.ValueRW.Distancesq = math.distancesq(characterPosition, entityPosition);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}