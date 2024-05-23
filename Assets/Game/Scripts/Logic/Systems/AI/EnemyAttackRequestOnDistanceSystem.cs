using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    //TODO: remake into separate systems for attack and movement 
    [UpdateInGroup(typeof(AISystemGroup))]
    public partial struct EnemyAttackRequestOnDistanceSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var weaponLengthLookup = SystemAPI.GetComponentLookup<WeaponLength>();
            var movementFlagsLookup = SystemAPI.GetComponentLookup<MovementFlags>();

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach ((RefRO<AttackTarget> attackTarget, RefRO<WeaponEntity> weaponEntity, Entity entity) in SystemAPI
                         .Query<RefRO<AttackTarget>, RefRO<WeaponEntity>>().WithEntityAccess())
            {
                if (!weaponLengthLookup.TryGetComponent(weaponEntity.ValueRO.Value, out WeaponLength weaponLength))
                {
                    continue;
                }

                if (weaponLength.Value * weaponLength.Value >= attackTarget.ValueRO.Distancesq)
                {
                    var request = ecb.CreateEntity();
                    ecb.AddComponent<AttackRequest>(request);
                    ecb.SetComponent(request, new AttackRequest() { Source = entity });
                    if (movementFlagsLookup.HasComponent(entity))
                    {
                        movementFlagsLookup.GetRefRW(entity).ValueRW.CanMove = false;
                    }
                }
                else
                {
                    if (movementFlagsLookup.HasComponent(entity))
                    {
                        movementFlagsLookup.GetRefRW(entity).ValueRW.CanMove = true;
                    }
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