using System;
using Unity.Collections;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct WeaponCollisionData : IComponentData
    {
        public NativeList<Entity> CollidedEntities;
    }
}