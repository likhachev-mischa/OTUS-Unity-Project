using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct MovementState : IComponentData
    {
        public MovementStates Value;
    }
}