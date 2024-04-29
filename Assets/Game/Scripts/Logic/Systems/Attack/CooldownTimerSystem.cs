using Game.Components;
using Unity.Burst;
using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial struct CooldownTimerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            foreach ((RefRW<AttackStates> attackState, RefRW<AttackCooldown> attackCooldown) in SystemAPI
                         .Query<RefRW<AttackStates>, RefRW<AttackCooldown>>())
            {
                if (attackState.ValueRO.IsOnCooldown)
                {
                    if (attackCooldown.ValueRO.Value <= 0)
                    {
                        attackCooldown.ValueRW.Value = attackCooldown.ValueRW.InitialValue.Value;
                        attackState.ValueRW.IsOnCooldown = false;
                    }
                    else
                    {
                        attackCooldown.ValueRW.Value -= deltaTime;
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