using System;
using DI;
using Game.Logic.Common;
using UnityEngine;

namespace Game.Logic.Installers
{
    [Serializable]
    public sealed class SpawnerInstaller : GameInstaller
    {
        [SerializeField]
        [Service(typeof(SpawnerController))]
        private SpawnerController spawnerController;

        [SerializeField]
        [Listener]
        private AutoSpawner autoSpawner;
    }
}