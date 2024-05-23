using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DI;
using SaveSystem.GameSavers;
using Sirenix.OdinInspector;

namespace SaveSystem
{
    [Serializable]
    public sealed class SaveLoadManager : IGameStartListener
    {
        private GameRepository gameRepository;
        private IGameSaver[] gameSavers;
        private Context gameContext;

        [Inject]
        private void Construct(GameRepository gameRepository, IGameSaver[] gameSavers, Context gameContext)
        {
            this.gameRepository = gameRepository;
            this.gameSavers = gameSavers;
            this.gameContext = gameContext;
        }

        [Button]
        public async UniTaskVoid Save()
        {
            Task[] tasks = new Task[gameSavers.Length];
            for (var i = 0; i < gameSavers.Length; i++)
            {
                IGameSaver gameSaver = gameSavers[i];
                tasks[i] = gameSaver.SaveData(gameRepository, gameContext).AsTask();
            }

            await Task.WhenAll(tasks);
            gameRepository.SetState();
        }

        [Button]
        public async UniTaskVoid Load()
        {
            gameRepository.GetState();
            Task[] tasks = new Task[gameSavers.Length];
            for (var i = 0; i < gameSavers.Length; i++)
            {
                IGameSaver gameSaver = gameSavers[i];
                tasks[i] = gameSaver.LoadData(gameRepository, gameContext).AsTask();
            }

            await Task.WhenAll(tasks);
        }

        void IGameStartListener.OnStart()
        {
            Load().Forget();
        }
    }
}