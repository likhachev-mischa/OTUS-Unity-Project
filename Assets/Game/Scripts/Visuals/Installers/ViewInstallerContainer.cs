using DI;
using UnityEngine;

namespace Game.Scripts.Visuals.Installers
{
    public sealed class ViewInstallerContainer : GameInstallerContainer
    {
        [SerializeField]
        [GameInstaller]
        private UIInstaller uiInstaller = new();
    }
}