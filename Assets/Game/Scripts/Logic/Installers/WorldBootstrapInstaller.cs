using DI;

namespace Game.Logic.Installers
{
    public sealed class WorldBootstrapInstaller : GameInstaller
    {
        [Service(typeof(WorldRegistry))]
        [Listener]
        private WorldRegistry worldRegistry = new();

        [Listener]
        private SystemsUpdateStateController systemsUpdateStateController = new();

        [Listener]
        private ContextEntityCreator contextEntityCreator = new();
    }
}