using Game.Components;
using SaveSystem.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(SaveLoadSystemGroup))]
    public partial struct EnemyLoadSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemyLoadEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var eventQuery = SystemAPI.QueryBuilder().WithAll<EnemyLoadEvent>().Build();
            state.EntityManager.DestroyEntity(eventQuery);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (EnemySaveAspect enemySaveAspect in SystemAPI.Query<EnemySaveAspect>())
            {
                var deathEventEntity = ecb.CreateEntity();
                ecb.AddComponent<DeathEvent>(deathEventEntity);
                ecb.SetComponent(deathEventEntity,
                    new DeathEvent() { Info = DeathInfo.INSTANT, Killed = enemySaveAspect.Self });
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