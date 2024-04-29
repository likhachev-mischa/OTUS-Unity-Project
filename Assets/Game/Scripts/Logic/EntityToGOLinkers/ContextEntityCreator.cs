using DI;
using Game.Components;
using Unity.Entities;

namespace Game.Logic
{
    public sealed class ContextEntityCreator : IInitializable
    {
        private WorldRegistry worldRegistry;
        private Context context;
        private IObjectResolver objectResolver;

        [Inject]
        private void Construct(WorldRegistry worldRegistry, Context context, IObjectResolver objectResolver)
        {
            this.worldRegistry = worldRegistry;
            this.context = context;
            this.objectResolver = objectResolver;
        }

        void IInitializable.Initialize()
        {
            EntityManager manager = worldRegistry.GetWorld(Worlds.MAIN).EntityManager;
            Entity entity = manager.CreateEntity();
            manager.AddComponentObject(entity,
                new ContextComponent() { Context = context, ObjectResolver = objectResolver });
        }
    }
}