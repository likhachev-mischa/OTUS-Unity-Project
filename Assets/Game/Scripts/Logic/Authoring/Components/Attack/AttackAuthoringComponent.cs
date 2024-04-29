using System;

namespace Game.Authoring
{
    [Serializable]
    public struct AttackAuthoringComponent
    {
        public bool CanAttack;
        public float Cooldown;
    }
}