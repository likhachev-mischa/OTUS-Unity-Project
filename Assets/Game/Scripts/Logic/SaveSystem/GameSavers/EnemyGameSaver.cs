using Cysharp.Threading.Tasks;
using Game.Authoring;
using Game.Utils;
using SaveSystem.Components;
using SaveSystem.SaveData;
using Unity.Mathematics;
using UnityEngine;

namespace SaveSystem.GameSavers
{
    public sealed class
        EnemyGameSaver : EntityGameSaver<EnemySaveDataContainer, SpawnerController, EnemySaveEvent, EnemyLoadEvent>
    {
        protected override async UniTask SetupData(EnemySaveDataContainer data, SpawnerController service)
        {
            EnemySpawnData[] array = data.SpawnData;
            foreach (EnemySpawnData enemySpawnData in array)
            {
                SerializableFloat3 position = enemySpawnData.TransformData.Position;
                SerializableFloat4 rotation = enemySpawnData.TransformData.Rotation;
                service.SendSpawnRequest(enemySpawnData.SpawnerID, new float3(position.x, position.y, position.z),
                    new quaternion(rotation.x, rotation.y, rotation.z, rotation.w), enemySpawnData.Health.Value);
            }

            await UniTask.DelayFrame(1);
        }
    }
}