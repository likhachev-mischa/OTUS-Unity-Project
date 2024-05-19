using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct SpawnerEntityPrefab : IComponentData
    {
        public Entity Prefab;
        public SpawnerID SpawnerId;
    }
}