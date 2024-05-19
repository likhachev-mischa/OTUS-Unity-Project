using Cysharp.Threading.Tasks;
using DI;

namespace SaveSystem.GameSavers
{
    public abstract class GameSaver<TData, TService> : IGameSaver where TService : class
    {
        public UniTask SaveData(GameRepository gameRepository, Context currentContext)
        {
            var service = currentContext.GetService<TService>();
            TData data = ConvertToData(service);
            gameRepository.SetData(data);
            return new UniTask();
        }

        public UniTask LoadData(GameRepository gameRepository, Context currentContext)
        {
            var service = currentContext.GetService<TService>();
            if (gameRepository.TryGetData(out TData data))
            {
                SetupData(data, service);
            }
            else
            {
                SetupDefaultData(service);
            }

            return new UniTask();
        }

        protected abstract TData ConvertToData(TService service);
        protected abstract void SetupData(TData data, TService service);

        protected virtual void SetupDefaultData(TService service)
        {
        }
    }
}