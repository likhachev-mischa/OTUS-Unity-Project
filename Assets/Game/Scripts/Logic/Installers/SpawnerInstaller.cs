using System;
using DI;
using Game.Authoring;
using UnityEngine;

namespace Game.Logic.Installers
{
    [Serializable]
    public sealed class SpawnerInstaller : GameInstaller
    {
        [SerializeField]
        [Service(typeof(SpawnerController))]
        private SpawnerController spawnerController;
    }
}