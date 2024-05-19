using System;
using UnityEngine;

namespace Game.Authoring
{
    [Serializable]
    public sealed class VisualProxyAuthoringComponent
    {
        [SerializeField]
        public GameObject ViewPrefab;
    }
}