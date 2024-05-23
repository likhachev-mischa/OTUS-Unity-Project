using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackRequestSystemGroup), OrderFirst = true)]
    public partial struct ControllerAttackActionEventHandlerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackActionPerformedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<AttackActionPerformedEvent>().Build();
            state.EntityManager.DestroyEntity(query);

            NativeArray<Entity> entities = SystemAPI.QueryBuilder().WithAll<ControllableTag>().Build()
                .ToEntityArray(Allocator.Temp);

            foreach (Entity entity in entities)
            {
                Entity request = state.EntityManager.CreateEntity();
                state.EntityManager.AddComponent<AttackRequest>(request);
                state.EntityManager.SetComponentData(request, new AttackRequest() { Source = entity });
            }

            entities.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}