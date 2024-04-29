using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Game.Components
{
    [Serializable]
    public struct MoveDirectionController : IComponentData
    {
        public float2 Value;
    }
}