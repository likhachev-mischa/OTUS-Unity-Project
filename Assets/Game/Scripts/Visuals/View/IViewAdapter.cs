using System;
using Unity.Entities;

namespace Game.Visuals
{
    public enum ComponentToDraw
    {
        ATTACK_COOLDOWN,
        HEALTH
    }

    public interface IViewAdapter
    {
        public Entity Entity { get; set; }
        public void DrawVisuals(ComponentToDraw component, string data);
        public void DisableVisuals(ComponentToDraw component);
    }
}