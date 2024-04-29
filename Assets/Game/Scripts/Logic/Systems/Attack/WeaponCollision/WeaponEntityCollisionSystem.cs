using Game.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Game.Systems.WeaponCollision
{
    [UpdateInGroup(typeof(WeaponCollisionSystemGroup))]
    public partial struct WeaponEntityCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            ComponentLookup<OwnerEntity> ownerLookup = SystemAPI.GetComponentLookup<OwnerEntity>();
            ComponentLookup<TeamComponent> teamLookup = SystemAPI.GetComponentLookup<TeamComponent>();
            ComponentLookup<WeaponFlagsComponent> weaponFlagsLookup =
                SystemAPI.GetComponentLookup<WeaponFlagsComponent>();

            var job = new WeaponEntityCollisionJob()
                { OwnerLookup = ownerLookup, TeamLookup = teamLookup, WeaponFlagsLookup = weaponFlagsLookup };
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
        public ComponentLookup<OwnerEntity> OwnerLookup;
        public ComponentLookup<TeamComponent> TeamLookup;
        public ComponentLookup<WeaponFlagsComponent> WeaponFlagsLookup;

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

            if (!WeaponFlagsLookup.TryGetComponent(weaponEntity, out WeaponFlagsComponent weaponFlags))
            {
                return;
            }

            Entity ownerEntity = ownerComponent.Value;
            if (!TeamLookup.TryGetComponent(attackedEntity, out TeamComponent attackedTeam) ||
                !TeamLookup.TryGetComponent(ownerEntity, out TeamComponent ownerTeam))
            {
                return;
            }

            if ((weaponFlags.Value & WeaponFlags.FRIENDLY_FIRE) == 0 &&
                ownerTeam.Value == attackedTeam.Value)
            {
                return;
            }

            Debug.Log("ATAKUEM BLED");
        }
    }
}