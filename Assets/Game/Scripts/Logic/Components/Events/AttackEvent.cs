using System;
using Unity.Entities;

namespace Game.Components
{
    /// <summary>
    /// Source - weapon
    /// </summary>
    [Serializable]
    public struct AttackEvent : IComponentData
    {
        public Entity Source;
        public Entity Target;
    }
}