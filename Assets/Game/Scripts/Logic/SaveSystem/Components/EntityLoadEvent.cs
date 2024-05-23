using System;
using Unity.Entities;

namespace SaveSystem.Components
{
    [Serializable]
    public struct EntityLoadEvent : IComponentData, IEntityLoadEvent
    {
    }
}