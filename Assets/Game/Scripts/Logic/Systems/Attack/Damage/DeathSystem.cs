using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(DamageCalculationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct DeathSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var eventQuery = SystemAPI.QueryBuilder().WithAll<DeathEvent>().Build();
            state.EntityManager.DestroyEntity(eventQuery);

            var healthLookup = SystemAPI.GetComponentLookup<Health>();
            var ownerLookup = SystemAPI.GetComponentLookup<OwnerEntity>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (RefRO<DealDamageEvent> dealDamageEvent in SystemAPI.Query<RefRO<DealDamageEvent>>())
            {
                var target = dealDamageEvent.ValueRO.Target;
                if (healthLookup.GetRefRO(target).ValueRO.Value > 0)
                {
                    continue;
                }

                var source = dealDamageEvent.ValueRO.Source;
                if (ownerLookup.TryGetComponent(source, out OwnerEntity ownerEntity))
                {
                    source = ownerEntity.Value;
                }

                Entity @event = ecb.CreateEntity();
                ecb.AddComponent(@event, new DeathEvent() { Killer = source, Killed = target });
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