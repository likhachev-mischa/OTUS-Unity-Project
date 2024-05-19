using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems.Spawner
{
    [UpdateInGroup(typeof(SpawnerSystemGroup))]
    public partial struct EnemySpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EnemySpawnRequest>();
            state.RequireForUpdate<SpawnerRepository>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var spawnerRepo = SystemAPI.GetSingleton<SpawnerRepository>();

            var transformLookup = SystemAPI.GetComponentLookup<LocalTransform>();
            var healthLookup = SystemAPI.GetComponentLookup<Health>();

            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach ((RefRO<EnemySpawnRequest> requestRO, Entity requestEntity) in SystemAPI
                         .Query<RefRO<EnemySpawnRequest>>().WithEntityAccess())
            {
                var request = requestRO.ValueRO;
                var prefab = spawnerRepo.Value[(int)request.SpawnData.SpawnerID];
                var entity = state.EntityManager.Instantiate(prefab);
                var transform = transformLookup.GetRefRW(entity);
                var transformData = request.SpawnData.TransformData;

                transform.ValueRW.Position = new float3(transformData.Position.x, transformData.Position.y,
                    transformData.Position.x);
                transform.ValueRW.Rotation.value = new float4(transformData.Rotation.x, transformData.Rotation.y,
                    transformData.Rotation.z, transformData.Rotation.w);

                if (healthLookup.HasComponent(entity))
                {
                    healthLookup.GetRefRW(entity).ValueRW = request.SpawnData.Health;
                }

                ecb.AddComponent<SpawnerIDComponent>(entity);
                ecb.SetComponent(entity,
                    new SpawnerIDComponent() { Value = request.SpawnData.SpawnerID });
                ecb.DestroyEntity(requestEntity);
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