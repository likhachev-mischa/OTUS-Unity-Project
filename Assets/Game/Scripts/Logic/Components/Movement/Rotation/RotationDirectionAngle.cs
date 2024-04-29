using System;
using Unity.Entities;

namespace Game.Components
{
    /// <summary>
    /// Angles for rotation specified in radians
    /// </summary>
    [Serializable]
    public struct RotationDirectionAngle : IComponentData
    {
        public float Initial;
        public float Destination;
    }
}