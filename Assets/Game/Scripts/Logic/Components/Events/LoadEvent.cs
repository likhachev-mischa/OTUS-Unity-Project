using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public class LoadEvent<TData, TService> : IComponentData
    {
        public TData Data;
        public TService Service;
        public bool IsDone;
    }
}