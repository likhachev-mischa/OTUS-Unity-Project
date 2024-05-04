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
            var query = SystemAPI.QueryBuilder().WithAll<AttackStartedEvent>().Build();
            var eventArray = query.ToEntityArray(Allocator.Temp);
            var eventLookup = SystemAPI.GetComponentLookup<AttackStartedEvent>();
            var weaponLookup = SystemAPI.GetComponentLookup<WeaponEntity>();
            var inactiveLookup = SystemAPI.GetComponentLookup<Inactive>();

            foreach (Entity entity in eventArray)
            {
                var source = eventLookup.GetRefRO(entity).ValueRO.Source;
                if (!weaponLookup.TryGetComponent(source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                if (inactiveLookup.HasComponent(weaponEntity.Value))
                {
                    state.EntityManager.RemoveComponent<Inactive>(weaponEntity.Value);
                }
            }

            eventArray.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}