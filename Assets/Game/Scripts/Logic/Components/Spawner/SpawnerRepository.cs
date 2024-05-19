using System;
using Unity.Collections;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct SpawnerRepository : IComponentData
    {
        public NativeHashMap<int, Entity> Value;
    }
}