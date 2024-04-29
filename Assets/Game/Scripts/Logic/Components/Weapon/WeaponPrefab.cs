using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct WeaponPrefab : IComponentData
    {
        public Entity Value;
    }
}