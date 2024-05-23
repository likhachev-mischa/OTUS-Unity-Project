using Game.Components;
using SaveSystem.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(SaveLoadSystemGroup))]
    public partial struct EntityLoadSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EntityLoadEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var eventQuery = SystemAPI.QueryBuilder().WithAll<EntityLoadEvent>().Build();
            state.EntityManager.DestroyEntity(eventQuery);

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (EntitySaveAspect entitySaveAspect in SystemAPI.Query<EntitySaveAspect>())
            {
                var deathEventEntity = ecb.CreateEntity();
                ecb.AddComponent<DeathEvent>(deathEventEntity);
                ecb.SetComponent(deathEventEntity,
                    new DeathEvent() { Info = DeathInfo.INSTANT, Killed = entitySaveAspect.Self });
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