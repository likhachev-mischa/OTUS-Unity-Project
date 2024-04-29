using Unity.Entities;
using Unity.Transforms;

namespace Game.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public sealed partial class MovementSystemGroup : ComponentSystemGroup
    {
    }
}