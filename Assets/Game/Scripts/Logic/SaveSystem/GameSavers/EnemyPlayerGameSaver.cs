using Cysharp.Threading.Tasks;
using Game.Components;
using Game.Logic.Common;
using Game.Utils;
using SaveSystem.Components;
using SaveSystem.SaveData;
using Unity.Mathematics;

namespace SaveSystem.GameSavers
{
    public sealed class
        EnemyPlayerGameSaver : EntityGameSaver<EntitySaveDataContainer, SpawnerController, EntitySaveEvent,
        EntityLoadEvent>
    {
        protected override async UniTask SetupData(EntitySaveDataContainer data, SpawnerController service)
        {
            EntitySpawnData[] array = data.SpawnData;
            foreach (EntitySpawnData entitySpawnData in array)
            {
                SerializableFloat3 position = entitySpawnData.TransformData.Position;
                SerializableFloat4 rotation = entitySpawnData.TransformData.Rotation;
                service.SendSpawnRequest(entitySpawnData.SpawnerID, new float3(position.x, position.y, position.z),
                    new quaternion(rotation.x, rotation.y, rotation.z, rotation.w), entitySpawnData.Health.Value);
            }

            await UniTask.DelayFrame(1);
        }

        protected override async UniTask SetupDefaultData(SpawnerController service)
        {
            service.SendSpawnRequest(SpawnerID.PLAYER, float3.zero, quaternion.identity, 100);
            await UniTask.DelayFrame(1);
        }
    }
}