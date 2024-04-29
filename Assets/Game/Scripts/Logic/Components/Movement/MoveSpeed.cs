using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct MoveSpeed : IComponentData
    {
        public float Value;

        public BlobAssetReference<float> InitialValue;
    }
}