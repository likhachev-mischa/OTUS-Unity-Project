using System;
using Game.Components;
using UnityEngine;

namespace Game.Authoring
{
    [Serializable]
    public struct WeaponStatsAuthoringComponent
    {
        [SerializeField]
        [Range(0, 30)]
        public float RotationSpeed;

        [SerializeField]
        public float Length;

        [SerializeField]
        public WeaponFlags WeaponFlags;
    }
}