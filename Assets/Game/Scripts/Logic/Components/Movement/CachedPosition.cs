using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Game.Components
{
    [Serializable]
    public struct CachedPosition : IComponentData
    {
        public float3 Value;
    }
}