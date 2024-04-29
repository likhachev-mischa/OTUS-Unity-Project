using Unity.Entities;

namespace Game.Components
{
    public struct AttackRequest : IComponentData
    {
        public Entity Source;
    }
}