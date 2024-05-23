using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(AttackSystemGroup))]
    public partial class AISystemGroup : ComponentSystemGroup
    {
    }
}