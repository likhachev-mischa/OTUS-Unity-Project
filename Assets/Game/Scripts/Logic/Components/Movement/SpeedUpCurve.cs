using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct SpeedUpCurve : IComponentData
    {
        public BlobAssetReference<Curve> Curve;
        public float SpeedUpTime;
        public float ElapsedTime;
    }
}