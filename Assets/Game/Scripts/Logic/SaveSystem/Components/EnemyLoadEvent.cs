using System;
using Unity.Entities;

namespace SaveSystem.Components
{
    [Serializable]
    public struct EnemyLoadEvent : IComponentData, IEntityLoadEvent
    {
    }
}