using Game.Utils;
using SaveSystem.Components;
using SaveSystem.SaveData;
using Unity.Collections;
using Unity.Entities;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(SaveLoadSystemGroup))]
    public partial class EnemySaveSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<EnemySaveEvent>();
        }

        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((EnemySaveEvent enemySaveEvent, Entity eventEntity) in SystemAPI.Query<EnemySaveEvent>()
                         .WithEntityAccess())
            {
                var enemyQuery = SystemAPI.QueryBuilder().WithAspect<EnemySaveAspect>().Build();
                var spawnData = new EnemySpawnData[enemyQuery.CalculateEntityCount()];
                var enemyArray = enemyQuery.ToEntityArray(Allocator.Temp);

                int index = 0;
                foreach (Entity entity in enemyArray)
                {
                    var aspect = SystemAPI.GetAspect<EnemySaveAspect>(entity);
                    spawnData[index] = new EnemySpawnData(aspect.Transform.ValueRO, aspect.Health.ValueRO,
                        aspect.SpawnerID.ValueRO.Value);
                    ++index;
                }

                enemySaveEvent.Data = new EnemySaveDataContainer(spawnData);
                enemySaveEvent.IsDone = true;
                ecb.DestroyEntity(eventEntity);
                enemyArray.Dispose();
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}