using Unity.Entities;
using Unity.Physics.Systems;

namespace Game.Systems
{
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial class WeaponCollisionSystemGroup : ComponentSystemGroup
    {
    }
}