using System;
using Unity.Entities;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public sealed class VisualTransform : IComponentData
    {
        public Transform Value;
    }
}