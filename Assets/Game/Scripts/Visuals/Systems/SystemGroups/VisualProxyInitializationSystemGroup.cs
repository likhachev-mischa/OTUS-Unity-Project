using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public sealed partial class VisualProxyInitializationSystemGroup : ComponentSystemGroup
    {
    }
}