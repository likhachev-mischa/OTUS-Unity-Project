using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct Curve
    {
        public BlobArray<float> Array;
    }
}