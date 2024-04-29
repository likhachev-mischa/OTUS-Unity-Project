using System;
using Unity.Entities;

namespace Game.Visuals
{
    public interface IViewAdapter
    {
        public Entity Entity { get; set; }
        public event Action VisualsDrawn;
        public void DrawVisuals();
    }
}