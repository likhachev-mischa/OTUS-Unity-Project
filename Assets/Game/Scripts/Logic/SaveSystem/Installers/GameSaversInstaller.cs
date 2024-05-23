using DI;
using SaveSystem.GameSavers;

namespace SaveSystem.Installers
{
    public sealed class GameSaversInstaller : GameInstaller
    {
        [ServiceCollection(typeof(IGameSaver))]
        private SystemGameSaver systemGameSaver = new();

        [ServiceCollection(typeof(IGameSaver))]
        private EnemyPlayerGameSaver enemyPlayerGameSaver = new();
    }
}