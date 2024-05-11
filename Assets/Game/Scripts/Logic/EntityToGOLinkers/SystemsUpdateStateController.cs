using DI;
using Game.Components;
using Unity.Core;
using Unity.Entities;

namespace Game.Logic
{
    public sealed class SystemsUpdateStateController : IInitializable, IGamePauseListener, IGameResumeListener,
        IGameUpdateListener
    {
        private WorldRegistry worldRegistry;
        private World mainWorld;
        private World eventWorld;
        private Entity singleton;

        [Inject]
        private void Construct(WorldRegistry worldRegistry)
        {
            this.worldRegistry = worldRegistry;
        }

        void IInitializable.Initialize()
        {
            mainWorld = worldRegistry.GetWorld(Worlds.MAIN);
            eventWorld = worldRegistry.GetWorld(Worlds.EVENT);
            singleton = mainWorld.EntityManager.CreateSingleton<GlobalPauseComponent>();
        }

        void IGamePauseListener.OnPause()
        {
            mainWorld.EntityManager.DestroyEntity(singleton);
        }

        void IGameResumeListener.OnResume()
        {
            singleton = mainWorld.EntityManager.CreateSingleton<GlobalPauseComponent>();
        }

        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            UpdateWorld(eventWorld, deltaTime);
        }

        private void UpdateWorld(World world, float deltaTime)
        {
            world.Time = new TimeData(world.Time.ElapsedTime + deltaTime, deltaTime);
            world.Update();
        }
    }
}