using System;
using DI;
using UnityEngine;

namespace Game.Logic.Installers
{
    [Serializable]
    public sealed class CamerasInstaller : GameInstaller
    {
        [Service(typeof(CamerasRegistry))]
        [SerializeField]
        private CamerasRegistry camerasRegistry = new();
    }
}