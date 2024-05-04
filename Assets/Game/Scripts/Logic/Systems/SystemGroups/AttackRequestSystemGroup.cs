using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup), OrderFirst = true)]
    public sealed partial class AttackRequestSystemGroup : ComponentSystemGroup
    {
    }
}