using DI;
using UnityEngine;

namespace Game.Logic.Installers
{
    public sealed class SceneInstallerContainer : GameInstallerContainer
    {
        [GameInstaller]
        private SceneInstaller sceneInstaller = new();

        [SerializeField]
        [GameInstaller]
        private CamerasInstaller camerasInstaller = new();

        [GameInstaller]
        private WorldBootstrapInstaller worldBootstrapInstaller = new();

        [SerializeField]
        [GameInstaller]
        private SpawnerInstaller spawnerInstaller;
    }
}