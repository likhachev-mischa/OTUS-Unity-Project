using System;
using Unity.Entities;

namespace Game.Components
{
    /// <summary>
    /// Angles in degrees for attack weapon to rotate around character
    /// </summary>
    [Serializable]
    public struct MeleeAttackAngle : IComponentData
    {
        public BlobAssetReference<float> StartPosition;
        public BlobAssetReference<float> TraversalAngle;
    }
}