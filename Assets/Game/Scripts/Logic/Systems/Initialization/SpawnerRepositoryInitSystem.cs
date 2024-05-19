using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Game.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnerRepositoryInitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnerEntityPrefab>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;
            RefRW<SpawnerRepository> repoRW;
            var query = SystemAPI.QueryBuilder().WithAll<SpawnerEntityPrefab>().Build();

            if (SystemAPI.HasSingleton<SpawnerRepository>())
            {
                repoRW = SystemAPI.GetSingletonRW<SpawnerRepository>();
                repoRW.ValueRW.Value.Dispose();
            }
            else
            {
                state.EntityManager.CreateSingleton<SpawnerRepository>();
                repoRW = SystemAPI.GetSingletonRW<SpawnerRepository>();
            }

            repoRW.ValueRW.Value = new NativeHashMap<int, Entity>(query.CalculateEntityCountWithoutFiltering(),
                Allocator.Persistent);

            var array = query.ToEntityArray(Allocator.Temp);
            var prefabLookup = SystemAPI.GetComponentLookup<SpawnerEntityPrefab>();

            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (Entity entity in array)
            {
                var spawnerEntityPrefab = prefabLookup.GetRefRO(entity);
                repoRW.ValueRW.Value.Add((int)spawnerEntityPrefab.ValueRO.SpawnerId,
                    spawnerEntityPrefab.ValueRO.Prefab);
                ecb.DestroyEntity(entity);
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
            array.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            if (SystemAPI.HasSingleton<SpawnerRepository>())
            {
                RefRW<SpawnerRepository> repoRW = SystemAPI.GetSingletonRW<SpawnerRepository>();
                repoRW.ValueRW.Value.Dispose();
            }
        }
    }
}