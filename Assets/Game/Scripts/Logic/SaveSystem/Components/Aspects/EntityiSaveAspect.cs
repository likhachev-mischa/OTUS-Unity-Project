using Game.Components;
using Unity.Entities;
using Unity.Transforms;

namespace SaveSystem.Components 
{
    public readonly partial struct EntityiSaveAspect : IAspect
    {
        public readonly Entity Self;
        public readonly RefRO<LocalTransform> Transform;
        public readonly RefRO<Health> Health;
        public readonly RefRO<SpawnerIDComponent> SpawnerID;
    }
}