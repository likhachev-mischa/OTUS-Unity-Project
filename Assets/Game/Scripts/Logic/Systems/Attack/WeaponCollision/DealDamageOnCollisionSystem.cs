using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup))]
    public partial struct DealDamageOnCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackCollisionEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var healthLookup = SystemAPI.GetComponentLookup<Health>();
            var damageLookup = SystemAPI.GetComponentLookup<Damage>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (RefRO<AttackCollisionEvent> attackCollisionEvent in SystemAPI.Query<RefRO<AttackCollisionEvent>>())
            {
                Entity weapon = attackCollisionEvent.ValueRO.Source;
                Entity target = attackCollisionEvent.ValueRO.Target;
                if (!damageLookup.TryGetComponent(weapon, out Damage damage) ||
                    !healthLookup.TryGetComponent(target, out Health health))
                {
                    continue;
                }

                var @event = ecb.CreateEntity();
                ecb.AddComponent(@event,
                    new DealDamageRequest() { Source = weapon, Target = target, Value = damage.Value });
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