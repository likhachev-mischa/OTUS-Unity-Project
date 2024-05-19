using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct SpawnerIDComponent : IComponentData
    {
        public SpawnerID Value;
    }
}