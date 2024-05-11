using Game.Systems;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [UpdateBefore(typeof(WeaponInitializationSystemGroup))]
    public sealed partial class VisualProxyInitializationSystemGroup : ComponentSystemGroup
    {
    }
}