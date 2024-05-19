using DI;

namespace SaveSystem.Installers
{
    public sealed class RepositoryInstaller : GameInstaller
    {
        [Service(typeof(GameRepository))]
        private GameRepository gameRepository = new();
    }
}