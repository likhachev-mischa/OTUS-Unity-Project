using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(WeaponCollisionHandleSystemGroup))]
    public partial class AttackEffectsSystemGroup : ComponentSystemGroup
    {
    }
}