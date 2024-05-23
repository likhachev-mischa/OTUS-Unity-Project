using System;
using SaveSystem.SaveData;
using Unity.Entities;

namespace SaveSystem.Components
{
    [Serializable]
    public class EntitySaveEvent : IComponentData, IEntitySaveEvent<EntitySaveDataContainer>
    {
        public EntitySaveDataContainer Data { get; set; }
        public bool IsDone { get; set; }
    }
}