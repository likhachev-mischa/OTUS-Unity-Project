using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct AttackStates : IComponentData
    {
        public bool CanAttack;
        public bool IsAttacking;
        public bool IsOnCooldown;
    }
}