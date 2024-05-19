using Unity.Entities;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class SaveLoadSystemGroup : ComponentSystemGroup
    {
    }
}