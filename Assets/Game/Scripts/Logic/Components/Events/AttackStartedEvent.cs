using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct AttackStartedEvent : IComponentData
    {
        public Entity Source;
    }
}