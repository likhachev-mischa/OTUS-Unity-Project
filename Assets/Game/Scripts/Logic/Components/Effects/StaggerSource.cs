using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct StaggerSource : IComponentData
    {
        public float Duration;
    }
}