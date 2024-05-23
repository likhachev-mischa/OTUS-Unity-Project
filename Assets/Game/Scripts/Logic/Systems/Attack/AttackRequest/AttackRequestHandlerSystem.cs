using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackRequestSystemGroup), OrderLast = true)]
    public partial struct AttackRequestHandlerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //state.RequireForUpdate<AttackRequest>();
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery requestQuery = SystemAPI.QueryBuilder().WithAll<AttackRequest>().Build();
            NativeArray<AttackRequest> requestArray = requestQuery.ToComponentDataArray<AttackRequest>(Allocator.Temp);

            EntityQuery eventQuery = SystemAPI.QueryBuilder().WithAll<AttackStartedEvent>().Build();
            state.EntityManager.DestroyEntity(eventQuery);

            foreach (AttackRequest attackRequest in requestArray)
            {
                Entity sourceEntity = attackRequest.Source;

                Entity attackEventEntity = state.EntityManager.CreateEntity();
                state.EntityManager.AddComponent<AttackStartedEvent>(attackEventEntity);
                state.EntityManager.SetComponentData(attackEventEntity,
                    new AttackStartedEvent() { Source = sourceEntity });
            }

            state.EntityManager.DestroyEntity(requestQuery);
            requestArray.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}