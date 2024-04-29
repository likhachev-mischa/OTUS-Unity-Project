using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct OwnerEntity : IComponentData
    {
        public Entity Value;
    }
}