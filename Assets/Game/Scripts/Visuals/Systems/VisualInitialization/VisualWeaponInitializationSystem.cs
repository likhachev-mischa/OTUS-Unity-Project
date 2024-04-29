using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(WeaponInitializationSystemGroup))]
    public partial class VisualWeaponInitializationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach ((_, RefRO<WeaponEntity> weaponEntity,
                         VisualTransform visualTransform) in SystemAPI
                         .Query<RefRO<WeaponInitializationRequest>, RefRO<WeaponEntity>, VisualTransform>())
            {
                Component weaponVisual = visualTransform.Value.GetComponentInChildren(typeof(WeaponViewAdapter));
                if (weaponVisual == null)
                {
                    continue;
                }

                IViewAdapter adapter = weaponVisual.GetComponent<WeaponViewAdapter>();
                adapter.Entity = weaponEntity.ValueRO.Value;
                ecb.AddComponent(weaponEntity.ValueRO.Value, new ViewAdapterComponent() { Value = adapter });
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}