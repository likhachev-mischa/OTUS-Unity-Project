using System;
using Game.Components;
using Game.Utils;
using Unity.Entities;
using UnityEngine;

namespace Game.Authoring
{
    [Serializable]
    public class MeleeAttackTypeAuthoringComponent : IAttackTypeAuthoring
    {
        [SerializeField]
        public float startAngle;

        [SerializeField]
        public float traversalAngle;

        public void Bake(IBaker baker, Entity entity)
        {
            var startAngleBlob = BlobUtils.CreateInitialComponent(startAngle);
            baker.AddBlobAsset(ref startAngleBlob, out _);
            var endAngleBlob = BlobUtils.CreateInitialComponent(-traversalAngle);
            baker.AddBlobAsset(ref endAngleBlob, out _);
            baker.AddComponent(entity,
                new MeleeAttackAngle() { StartPosition = startAngleBlob, TraversalAngle = endAngleBlob });
        }
    }
}