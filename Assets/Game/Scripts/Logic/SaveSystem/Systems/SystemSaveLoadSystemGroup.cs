using Game.Systems;
using Unity.Entities;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(EventCleanupSystemGroup), OrderLast = true)]
    public partial class SystemSaveLoadSystemGroup : ComponentSystemGroup
    {
    }
}