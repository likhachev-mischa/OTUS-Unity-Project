using System;
using Game.Utils;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct EnemySpawnRequest : IComponentData
    {
        public EntitySpawnData SpawnData;
    }
}