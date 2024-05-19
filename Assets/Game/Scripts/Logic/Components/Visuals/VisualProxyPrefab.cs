using System;
using Game.Authoring;
using Unity.Entities;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public sealed class VisualProxyPrefab : IComponentData
    {
        public GameObject Value;
    }
}