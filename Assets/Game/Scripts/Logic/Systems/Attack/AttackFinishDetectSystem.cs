using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct AttackFinishDetectSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //state.RequireForUpdate<RotationFinishedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var eventQuery = SystemAPI.QueryBuilder().WithAll<AttackFinishedEvent>().Build();
            state.EntityManager.DestroyEntity(eventQuery);
            var query = SystemAPI.QueryBuilder().WithAll<RotationFinishedEvent>().Build();
            var eventArray = query.ToEntityArray(Allocator.Temp);
            var ownerLookup = SystemAPI.GetComponentLookup<OwnerEntity>();
            var eventLookup = SystemAPI.GetComponentLookup<RotationFinishedEvent>();

            foreach (Entity entity in eventArray)
            {
                Entity source = eventLookup.GetRefRO(entity).ValueRO.Source;
                var owner = ownerLookup.GetRefRO(source).ValueRO.Value;
                state.EntityManager.AddComponent<Inactive>(source);
                var @event = state.EntityManager.CreateEntity();
                state.EntityManager.AddComponent<AttackFinishedEvent>(@event);
                state.EntityManager.SetComponentData(@event, new AttackFinishedEvent() { Source = owner });
            }

            eventArray.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}