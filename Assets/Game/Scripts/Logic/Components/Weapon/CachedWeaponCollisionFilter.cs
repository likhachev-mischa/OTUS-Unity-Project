using Unity.Entities;

namespace Game.Components
{
    public struct CachedWeaponCollisionFilter : IComponentData
    {
        public uint BelongsTo;
        public uint CollidesWith;
    }
}