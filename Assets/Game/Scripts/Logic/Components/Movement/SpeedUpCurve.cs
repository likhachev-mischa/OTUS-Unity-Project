using System;
using Unity.Entities;

namespace Game.Components
{
    [Serializable]
    public struct SpeedUpCurve : IComponentData
    {
        public BlobAssetReference<Curve> Curve;
        public BlobAssetReference<float> SpeedUpTime;
        public float ElapsedTime;
    }
}