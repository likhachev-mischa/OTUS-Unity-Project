using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class SpawnerSystemGroup : ComponentSystemGroup
    {
    }
}