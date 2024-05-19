using DI;

namespace SaveSystem.Installers
{
    public sealed class SceneSaveSystemInstallerContainer : GameInstallerContainer
    {
        [GameInstaller]
        private GameSaversInstaller gameSaversInstaller = new();
    }
}