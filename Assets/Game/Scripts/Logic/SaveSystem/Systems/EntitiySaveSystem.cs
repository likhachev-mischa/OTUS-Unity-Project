using Game.Utils;
using SaveSystem.Components;
using SaveSystem.SaveData;
using Unity.Collections;
using Unity.Entities;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(SaveLoadSystemGroup))]
    public partial class EntitiySaveSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<EntitySaveEvent>();
        }

        protected override void OnUpdate()
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach ((EntitySaveEvent entitySaveEvent, Entity eventEntity) in SystemAPI.Query<EntitySaveEvent>()
                         .WithEntityAccess())
            {
                var enemyQuery = SystemAPI.QueryBuilder().WithAspect<EntitySaveAspect>().Build();
                var spawnData = new EntitySpawnData[enemyQuery.CalculateEntityCount()];
                var enemyArray = enemyQuery.ToEntityArray(Allocator.Temp);

                int index = 0;
                foreach (Entity entity in enemyArray)
                {
                    var aspect = SystemAPI.GetAspect<EntitySaveAspect>(entity);
                    spawnData[index] = new EntitySpawnData(aspect.Transform.ValueRO, aspect.Health.ValueRO,
                        aspect.SpawnerID.ValueRO.Value);
                    ++index;
                }

                entitySaveEvent.Data = new EntitySaveDataContainer(spawnData);
                entitySaveEvent.IsDone = true;
                ecb.DestroyEntity(eventEntity);
                enemyArray.Dispose();
            }

            ecb.Playback(EntityManager);
            ecb.Dispose();
        }
    }
}