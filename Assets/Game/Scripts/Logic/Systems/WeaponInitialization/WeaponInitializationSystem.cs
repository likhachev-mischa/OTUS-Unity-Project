using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponInitializationSystemGroup), OrderFirst = true)]
    public partial struct WeaponInitializationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WeaponInitializationRequest>();
        }

        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var singleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer delayedEcb = singleton.CreateCommandBuffer(state.World.Unmanaged);
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((_, RefRO<WeaponPrefab> prefab, RefRO<LocalTransform> transform, Entity entity) in SystemAPI
                         .Query<RefRO<WeaponInitializationRequest>, RefRO<WeaponPrefab>, RefRO<LocalTransform>>()
                         .WithEntityAccess())
            {
                delayedEcb.RemoveComponent<WeaponInitializationRequest>(entity);
                Entity weaponEntity = ecb.Instantiate(prefab.ValueRO.Value);

                ecb.AddComponent(entity, new WeaponEntity() { Value = weaponEntity });
                ecb.AddComponent(weaponEntity, new Parent() { Value = entity });
                ecb.AddComponent(weaponEntity, new OwnerEntity() { Value = entity });
                ecb.AddComponent(weaponEntity, new WeaponCollisionData());
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