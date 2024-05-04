using Game.Components;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionSystemGroup))]
    public partial struct CollisionEventTestSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var VARIABLE in SystemAPI.Query<RefRO<AttackCollisionEvent>>())
            {
                Debug.Log("event");
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}