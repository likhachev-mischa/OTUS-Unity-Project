using DI;
using Unity.Entities;

namespace Game.Logic
{
    public enum Worlds
    {
        MAIN,
        EVENT
    }

    public sealed class WorldRegistry : ILateLoadListener
    {
        private World mainWorld;
        private World eventWorld;

        [Inject]
        private void Construct()
        {
            mainWorld = World.DefaultGameObjectInjectionWorld;
            ScriptBehaviourUpdateOrder.RemoveWorldFromCurrentPlayerLoop(mainWorld);

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


        public void OnLateLoad()
        {
            ScriptBehaviourUpdateOrder.AppendWorldToCurrentPlayerLoop(mainWorld);
        }
    }
}