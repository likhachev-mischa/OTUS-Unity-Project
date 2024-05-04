using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))][UpdateAfter(typeof(WeaponCollisionHandleSystemGroup))]
    public partial class DamageCalculationSystemGroup : ComponentSystemGroup
    {
    }
}