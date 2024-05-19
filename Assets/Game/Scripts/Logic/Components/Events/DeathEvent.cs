using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct DeathEvent : IComponentData
    {
        public Entity Killed;
        public Entity Killer;
        public DeathInfo Info;
    }
}