using System;
using Game.Logic.Common;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public class ObjectPoolComponent : IComponentData
    {
        public ObjectPoolDirectory Value;
    }
}