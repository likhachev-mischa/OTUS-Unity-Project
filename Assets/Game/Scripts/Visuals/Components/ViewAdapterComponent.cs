using System;
using Unity.Entities;

namespace Game.Visuals.Components
{
    [Serializable]
    public sealed class ViewAdapterComponent : IComponentData
    {
        public IViewAdapter Value;
    }
}