using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct WeaponLength : IComponentData
    {
        public float Value;
    }
}