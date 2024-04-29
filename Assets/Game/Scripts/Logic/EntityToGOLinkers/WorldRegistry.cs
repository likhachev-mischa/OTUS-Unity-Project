using DI;
using Unity.Entities;

namespace Game.Logic
{
    public enum Worlds
    {
        MAIN,
        EVENT
    }

    public sealed class WorldRegistry
    {
        private World mainWorld;
        private World eventWorld;

        [Inject]
        private void Construct()
        {
            mainWorld = World.DefaultGameObjectInjectionWorld;

            eventWorld = new World("Event World", WorldFlags.None);
        }

        public World GetWorld(Worlds type)
        {
            return type switch
            {
                Worlds.MAIN => mainWorld,
                Worlds.EVENT => eventWorld,
                _ => mainWorld
            };
        }
    }
}