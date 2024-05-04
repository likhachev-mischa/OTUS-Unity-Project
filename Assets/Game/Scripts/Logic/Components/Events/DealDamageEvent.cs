using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct DealDamageEvent : IComponentData
    {
        public Entity Source;
        public Entity Target;
        public float Value;
    }
}