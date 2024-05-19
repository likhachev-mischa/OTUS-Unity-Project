using System;
using Game.Utils;

namespace SaveSystem.SaveData
{
    [Serializable]
    public struct EnemySaveDataContainer
    {
        public EnemySpawnData[] SpawnData;

        public EnemySaveDataContainer(EnemySpawnData[] spawnData)
        {
            SpawnData = spawnData;
        }
    }
}