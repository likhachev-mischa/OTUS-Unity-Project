using DI;
using UnityEngine;

namespace SaveSystem.Installers
{
    public sealed class SceneSaveSystemInstallerContainer : GameInstallerContainer
    {
        [GameInstaller]
        private GameSaversInstaller gameSaversInstaller = new();

        [GameInstaller]
        [SerializeField]
        private SaveLoadManagerInstaller saveLoadManagerInstaller = new();
    }
}