using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Game.Components
{
    [Serializable]
    public struct AttackTarget : IComponentData
    {
        public float3 Position;
        public float Distancesq;
    }
}