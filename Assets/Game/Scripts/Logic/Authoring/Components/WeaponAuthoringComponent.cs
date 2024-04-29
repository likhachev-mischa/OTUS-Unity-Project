using System;
using UnityEngine;

namespace Game.Authoring
{
    [Serializable]
    public sealed class WeaponAuthoringComponent
    {
        [SerializeField]
        public WeaponAuthoring weapon;
    }
}