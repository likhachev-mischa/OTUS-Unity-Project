using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Game.Components
{
    [Serializable]
    [WriteGroup(typeof(Unity.Transforms.LocalTransform))]
    public struct RotationDirection : IComponentData
    {
        public float3 Value;
    }
}