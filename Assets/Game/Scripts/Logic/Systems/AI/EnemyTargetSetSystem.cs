using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AISystemGroup), OrderFirst = true)]
    [RequireMatchingQueriesForUpdate]
    public partial struct EnemyTargetSetSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CharacterTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((_, Entity entity) in SystemAPI.Query<RefRO<EnemyTag>>()
                         .WithNone<AttackTarget>().WithEntityAccess())
            {
                ecb.AddComponent<AttackTarget>(entity);
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}