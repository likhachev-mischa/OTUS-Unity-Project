using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackRequestSystemGroup))]
    public partial struct AttackRequestValidationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackRequest>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ComponentLookup<Inactive> inactiveLookup = SystemAPI.GetComponentLookup<Inactive>();
            ComponentLookup<AttackStates> statesLookup = SystemAPI.GetComponentLookup<AttackStates>();

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach ((RefRO<AttackRequest> attackRequest, Entity requestEntity) in SystemAPI
                         .Query<RefRO<AttackRequest>>().WithEntityAccess())
            {
                Entity sourceEntity = attackRequest.ValueRO.Source;

                if (inactiveLookup.HasComponent(sourceEntity))
                {
                    ecb.DestroyEntity(requestEntity);
                    continue;
                }

                if (statesLookup.TryGetComponent(sourceEntity, out AttackStates
                        attackState))
                {
                    if (attackState.IsOnCooldown || !attackState.CanAttack) //|| attackState.IsAttacking)
                    {
                        ecb.DestroyEntity(requestEntity);
                        continue;
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