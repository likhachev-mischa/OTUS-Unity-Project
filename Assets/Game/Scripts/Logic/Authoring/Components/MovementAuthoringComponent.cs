using System;
using UnityEngine;

namespace Game.Authoring
{
    [Serializable]
    public struct MovementAuthoringComponent
    {
        [SerializeField]
        public float MovementSpeed;

        [SerializeField]
        public float RotationSpeed;

        [SerializeField]
        public float AccelerationTime;

        [SerializeField]
        public AnimationCurve AccelerationCurve;

        [SerializeField]
        public float CurvePrecision;
    }
}