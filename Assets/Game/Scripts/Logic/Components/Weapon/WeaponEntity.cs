using System;
using Unity.Entities;
using Unity.Transforms;

namespace Game.Components
{
    [Serializable]
    public struct WeaponEntity : IComponentData
    {
        public Entity Value;
    }
}