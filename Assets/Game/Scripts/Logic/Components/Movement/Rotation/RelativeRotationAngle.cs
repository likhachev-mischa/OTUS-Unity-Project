using System;
using Unity.Entities;

namespace Game.Components
{
    /// <summary>
    /// angle in radians for object to rotate in front of parent
    /// </summary>
    [Serializable]
    public struct RelativeRotationAngle : IComponentData
    {
        public float Value;
    }
}