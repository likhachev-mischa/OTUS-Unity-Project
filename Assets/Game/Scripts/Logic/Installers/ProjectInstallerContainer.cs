using DI;

namespace Game.Logic.Installers
{
    public sealed class ProjectInstallerContainer : GameInstallerContainer
    {
        [GameInstaller]
        private ProjectInstaller projectInstaller = new();

       
    }
}