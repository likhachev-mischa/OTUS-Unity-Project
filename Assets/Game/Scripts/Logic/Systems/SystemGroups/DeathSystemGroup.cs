using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    [UpdateAfter(typeof(DamageCalculationSystemGroup))]
    public partial class DeathSystemGroup : ComponentSystemGroup
    {
    }
}