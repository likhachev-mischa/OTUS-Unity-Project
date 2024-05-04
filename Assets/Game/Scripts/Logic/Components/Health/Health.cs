﻿using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Health : IComponentData
    {
        public float Value;
        //public BlobAssetReference<float> InitialValue;
    }
}