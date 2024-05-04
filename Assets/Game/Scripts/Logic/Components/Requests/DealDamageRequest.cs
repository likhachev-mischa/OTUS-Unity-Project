using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct DealDamageRequest : IComponentData
    {
        public Entity Source;
        public Entity Target;
        public float Value;
    }
}