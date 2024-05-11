using System;
using Unity.Entities;
using UnityEngine;

namespace Game.Authoring
{
    public abstract class AttackTypeAuthoring : MonoBehaviour
    {
        public abstract void Bake(IBaker baker, Entity entity);
    }
}