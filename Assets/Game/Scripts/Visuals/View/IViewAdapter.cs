using System;
using Unity.Entities;

namespace Game.Visuals
{
    public interface IViewAdapter
    {
        public Entity Entity { get; set; }
        public void DrawVisuals();
    }
}