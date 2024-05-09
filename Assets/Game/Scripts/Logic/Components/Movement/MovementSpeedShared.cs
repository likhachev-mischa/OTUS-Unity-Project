using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct MovementSpeedShared : ISharedComponentData
    {
        public float Value;
    }
}