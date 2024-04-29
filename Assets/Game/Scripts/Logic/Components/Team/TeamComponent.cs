using Unity.Entities;

namespace Game.Components
{
    public struct TeamComponent : IComponentData
    {
        public Team Value;
    }
}