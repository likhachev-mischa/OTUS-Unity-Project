using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionSystemGroup), OrderFirst = true)]
    public partial struct WeaponEntityCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ComponentLookup<OwnerEntity> ownerLookup = SystemAPI.GetComponentLookup<OwnerEntity>();
            ComponentLookup<TeamComponent> teamLookup = SystemAPI.GetComponentLookup<TeamComponent>();
            ComponentLookup<WeaponFlagsComponent> weaponFlagsLookup =
                SystemAPI.GetComponentLookup<WeaponFlagsComponent>();

            var singleton = SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = singleton.CreateCommandBuffer(state.WorldUnmanaged);
            var job = new WeaponEntityCollisionJob()
            {
                OwnerLookup = ownerLookup, TeamLookup = teamLookup, WeaponFlagsLookup = weaponFlagsLookup, ECB = ecb
            };
            state.Dependency = job.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public struct WeaponEntityCollisionJob : ITriggerEventsJob
    {
        [ReadOnly]
        public ComponentLookup<OwnerEntity> OwnerLookup;

        [ReadOnly]
        public ComponentLookup<TeamComponent> TeamLookup;

        [ReadOnly]
        public ComponentLookup<WeaponFlagsComponent> WeaponFlagsLookup;

        public EntityCommandBuffer ECB;

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity weaponEntity;
            Entity attackedEntity;
            if (OwnerLookup.TryGetComponent(triggerEvent.EntityA, out OwnerEntity ownerComponent))
            {
                weaponEntity = triggerEvent.EntityA;
                attackedEntity = triggerEvent.EntityB;
            }
            else if (OwnerLookup.TryGetComponent(triggerEvent.EntityB, out ownerComponent))
            {
                weaponEntity = triggerEvent.EntityB;
                attackedEntity = triggerEvent.EntityA;
            }
            else
            {
                return;
            }


            var @event = ECB.CreateEntity();
            ECB.AddComponent(@event, new AttackCollisionEvent() { Source = weaponEntity, Target = attackedEntity });
        }
    }
}