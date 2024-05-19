using Cysharp.Threading.Tasks;
using DI;
using Game.Logic;
using SaveSystem.Components;
using Unity.Entities;

namespace SaveSystem.GameSavers
{
    public sealed class SystemGameSaver : IGameSaver
    {
        private EntityManager entityManager;

        [Inject]
        private void Construct(WorldRegistry worldRegistry)
        {
            this.entityManager = worldRegistry.GetWorld(Worlds.MAIN).EntityManager;
        }

        public async UniTask SaveData(GameRepository gameRepository, Context currentContext)
        {
            var eventEntity = entityManager.CreateEntity();
            entityManager.AddComponent<SystemSaveEvent>(eventEntity);
            await UniTask.DelayFrame(1);
        }

        public async UniTask LoadData(GameRepository gameRepository, Context currentContext)
        {
            var eventEntity = entityManager.CreateEntity();
            entityManager.AddComponent<SystemLoadEvent>(eventEntity);
            await UniTask.DelayFrame(1);
        }
    }
}