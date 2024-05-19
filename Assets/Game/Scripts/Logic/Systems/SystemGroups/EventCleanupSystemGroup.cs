using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial class EventCleanupSystemGroup : ComponentSystemGroup
    {
    }
}