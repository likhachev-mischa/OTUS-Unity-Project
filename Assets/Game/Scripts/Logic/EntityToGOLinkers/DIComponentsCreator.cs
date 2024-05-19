using DI;
using Game.Components;
using Game.Logic.Common;
using Unity.Entities;

namespace Game.Logic
{
    public sealed class DIComponentsCreator : IInitializable
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

            CreateRegistryComponent(manager);
            CreateObjectPoolComponent(manager);
        }


        private void CreateRegistryComponent(EntityManager manager)
        {
            Entity entity = manager.CreateEntity();
            manager.AddComponentObject(entity,
                new ContextComponent() { Context = context, ObjectResolver = objectResolver });
        }

        private void CreateObjectPoolComponent(EntityManager manager)
        {
            ObjectPoolDirectory objectPoolDirectory = new(objectResolver);
            context.BindService(typeof(ObjectPoolDirectory), objectPoolDirectory);
            Entity entity = manager.CreateEntity();
            manager.AddComponentObject(entity,
                new ObjectPoolComponent() { Value = objectPoolDirectory });
        }
    }
}