using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    [WorldSystemFilter(WorldSystemFilterFlags.BakingSystem)]
    public partial struct WeaponInitializationBakingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WeaponTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((RefRO<WeaponTag> weaponTag, RefRW<LocalTransform> transform, Entity entity) in SystemAPI
                         .Query<RefRO<WeaponTag>, RefRW<LocalTransform>>().WithEntityAccess())
            {
                ecb.AddComponent<CachedPosition>(entity);
                float3 currentPos = transform.ValueRO.Position;
                ecb.SetComponent(entity, new CachedPosition() { Value = currentPos });
                transform.ValueRW.Position = new float3(0, -100, 0);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}