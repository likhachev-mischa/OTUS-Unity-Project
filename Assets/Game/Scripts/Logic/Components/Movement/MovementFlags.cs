using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct MovementFlags : IComponentData
    {
        public bool IsMoving;
        public bool CanMove;
    }
}