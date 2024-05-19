﻿using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Damage : IComponentData
    {
        public int Value;
    }
}