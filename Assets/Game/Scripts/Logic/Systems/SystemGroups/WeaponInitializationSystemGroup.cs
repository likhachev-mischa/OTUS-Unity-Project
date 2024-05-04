using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public sealed partial class WeaponInitializationSystemGroup : ComponentSystemGroup
    {
    }
}