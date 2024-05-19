using DI;

namespace SaveSystem.Installers
{
    public sealed class ProjectSaveSystemInstallerContainer : GameInstallerContainer
    {
        [GameInstaller]
        private RepositoryInstaller repositoryInstaller = new();
    }
}