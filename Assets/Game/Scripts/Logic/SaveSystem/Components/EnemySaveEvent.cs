using System;
using SaveSystem.SaveData;
using Unity.Entities;

namespace SaveSystem.Components
{
    [Serializable]
    public class EnemySaveEvent : IComponentData, IEntitySaveEvent<EnemySaveDataContainer>
    {
        public EnemySaveDataContainer Data { get; set; }
        public bool IsDone { get; set; }
    }
}