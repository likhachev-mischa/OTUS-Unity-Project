using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    public partial class WeaponCollisionHandleSystemGroup : ComponentSystemGroup
    {
    }
}