using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct AttackCooldownShared : ISharedComponentData
    {
        public float Value;
    }
}