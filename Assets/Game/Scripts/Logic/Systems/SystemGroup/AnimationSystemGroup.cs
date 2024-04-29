using Unity.Entities;

namespace Game.Systems
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public sealed partial class AnimationSystemGroup : ComponentSystemGroup
    {
    }
}