using Cysharp.Threading.Tasks;
using DI;
using Game.Logic;
using SaveSystem.Components;
using Unity.Entities;

namespace SaveSystem.GameSavers
{
    public abstract class EntityGameSaver<TData, TService, TSaveEvent, TLoadEvent> : IGameSaver
        where TService : class
        where TSaveEvent : class, IEntitySaveEvent<TData>, IComponentData, new()
        where TLoadEvent : struct, IEntityLoadEvent, IComponentData

    {
        protected EntityManager entityManager;
        private TSaveEvent entitySaveEvent = new();

        [Inject]
        protected void Construct(WorldRegistry worldRegistry)
        {
            this.entityManager = worldRegistry.GetWorld(Worlds.MAIN).EntityManager;
        }

        public async UniTask SaveData(GameRepository gameRepository, Context currentContext)
        {
            var service = currentContext.GetService<TService>();
            await ConvertToData(service);
            entitySaveEvent.IsDone = false;
            gameRepository.SetData(entitySaveEvent.Data);
        }

        public async UniTask LoadData(GameRepository gameRepository, Context currentContext)
        {
            var service = currentContext.GetService<TService>();
            var eventEntity = entityManager.CreateEntity();
            entityManager.AddComponent<TLoadEvent>(eventEntity);
            if (gameRepository.TryGetData(out TData data))
            {
                await SetupData(data, service);
            }
            else
            {
                await SetupDefaultData(service);
            }
        }

        private async UniTask ConvertToData(TService service)
        {
            var eventEntity = entityManager.CreateEntity();
            entityManager.AddComponentData(eventEntity, entitySaveEvent);
            await UniTask.WaitUntil(IsSaveDone);
        }

        protected abstract UniTask SetupData(TData data, TService service);

        protected virtual UniTask SetupDefaultData(TService service)
        {
            return new UniTask();
        }

        private bool IsSaveDone()
        {
            return entitySaveEvent.IsDone;
        }
    }
}