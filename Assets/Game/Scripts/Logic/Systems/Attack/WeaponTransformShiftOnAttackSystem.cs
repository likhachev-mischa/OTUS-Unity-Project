using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct WeaponTransformShiftOnAttackSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>();
            var cacheLookup = SystemAPI.GetComponentLookup<CachedPosition>();
            var weaponLookup = SystemAPI.GetComponentLookup<WeaponEntity>();
            state.Dependency.Complete();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (RefRO<AttackStartedEvent> attackStartedEvent in SystemAPI.Query<RefRO<AttackStartedEvent>>())
            {
                if (!weaponLookup.TryGetComponent(attackStartedEvent.ValueRO.Source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                Entity weapon = weaponEntity.Value;
                var transform = transformLookup.GetRefRW(weapon);
                transform.ValueRW.Position = cacheLookup.GetRefRO(weapon).ValueRO.Value;
                ecb.RemoveComponent<CachedPosition>(weapon);
            }

            foreach (RefRO<AttackFinishedEvent> attackFinishedEvent in SystemAPI.Query<RefRO<AttackFinishedEvent>>())
            {
                if (!weaponLookup.TryGetComponent(attackFinishedEvent.ValueRO.Source, out WeaponEntity weaponEntity))
                {
                    continue;
                }

                Entity weapon = weaponEntity.Value;
                var transform = transformLookup.GetRefRW(weapon);
                ecb.AddComponent(weapon, new CachedPosition() { Value = transform.ValueRO.Position });
                transform.ValueRW.Position = new float3(0, -100, 0);
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