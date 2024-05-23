using Game.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct CooldownTimerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<GlobalPauseComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var cooldownLookup = SystemAPI.GetComponentLookup<AttackCooldown>();
            foreach ((RefRW<AttackStates> attackState, Entity entity) in SystemAPI
                         .Query<RefRW<AttackStates>>().WithEntityAccess())
            {
                if (attackState.ValueRO.IsOnCooldown)
                {
                    var attackCooldown = cooldownLookup.GetRefRW(entity);
                    if (attackCooldown.ValueRO.Value <= 0)
                    {
                        attackCooldown.ValueRW.Value =
                            state.EntityManager.GetSharedComponent<AttackCooldownShared>(entity).Value;
                        attackState.ValueRW.IsOnCooldown = false;
                    }
                    else
                    {
                        attackCooldown.ValueRW.Value = math.max(0, attackCooldown.ValueRO.Value - deltaTime);
                    }
                }
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}