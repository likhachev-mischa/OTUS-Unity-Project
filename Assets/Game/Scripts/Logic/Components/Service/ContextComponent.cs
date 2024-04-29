using System;
using DI;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public class ContextComponent : IComponentData
    {
        public Context Context;
        public IObjectResolver ObjectResolver;
    }
}