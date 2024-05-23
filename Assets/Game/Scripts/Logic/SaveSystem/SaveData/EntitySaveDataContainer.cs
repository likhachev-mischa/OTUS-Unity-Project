using System;
using Game.Utils;

namespace SaveSystem.SaveData
{
    [Serializable]
    public struct EntitySaveDataContainer
    {
        public EntitySpawnData[] SpawnData;

        public EntitySaveDataContainer(EntitySpawnData[] spawnData)
        {
            SpawnData = spawnData;
        }
    }
}