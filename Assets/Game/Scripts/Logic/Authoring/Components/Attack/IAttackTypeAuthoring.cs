using System;
using Unity.Entities;

namespace Game.Authoring
{
    public interface IAttackTypeAuthoring
    {
        public void Bake(IBaker baker, Entity entity);
    }
}