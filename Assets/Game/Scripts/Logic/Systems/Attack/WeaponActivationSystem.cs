using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct WeaponActivationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackStartedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var weaponLookup = SystemAPI.GetComponentLookup<WeaponEntity>();
            var inactiveLookup = SystemAPI.GetComponentLookup<Inactive>();

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (RefRO<AttackStartedEvent> attackStartedEvent in SystemAPI.Query<RefRO<AttackStartedEvent>>())
            {
                var source = attackStartedEvent.ValueRO.Source;
                if (!weaponLookup.TryGetComponent(source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                if (inactiveLookup.HasComponent(weaponEntity.Value))
                {
                    ecb.RemoveComponent<Inactive>(weaponEntity.Value);
                }
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