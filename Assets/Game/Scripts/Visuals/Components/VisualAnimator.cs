using System;
using Unity.Entities;

namespace Game.Visuals.Components
{
    [Serializable]
    public sealed class VisualAnimator : IComponentData
    {
        public EntityAnimator Value;
    }
}