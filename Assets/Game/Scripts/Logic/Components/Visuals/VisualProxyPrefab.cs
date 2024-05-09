using System;
using Game.Authoring;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public sealed class VisualProxyPrefab : IComponentData
    {
        public VisualProxy Value;
    }
}