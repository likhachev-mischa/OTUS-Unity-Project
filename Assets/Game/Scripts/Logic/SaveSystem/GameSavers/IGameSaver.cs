
using Cysharp.Threading.Tasks;
using DI;

namespace SaveSystem.GameSavers
{
    public interface IGameSaver
    {
        public UniTask SaveData(GameRepository gameRepository, Context currentContext);
        public UniTask LoadData(GameRepository gameRepository, Context currentContext);
    }
}