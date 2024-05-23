using System;
using DI;
using UnityEngine;

namespace SaveSystem.Installers
{
    [Serializable]
    public sealed class SaveLoadManagerInstaller : GameInstaller
    {
        [SerializeField]
        [Listener]
        [Service(typeof(SaveLoadManager))]
        private SaveLoadManager saveLoadManager = new();
    }
}