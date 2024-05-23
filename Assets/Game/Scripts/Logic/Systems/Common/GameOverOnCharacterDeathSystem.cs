using Game.Components;
using Unity.Entities;

namespace Game.Systems.Common
{
    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial class GameOverOnCharacterDeathSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<ContextComponent>();
            RequireForUpdate<DeathEvent>();
        }

        protected override void OnUpdate()
        {
            var characterEntity = SystemAPI.GetSingletonEntity<CharacterTag>();
            var contextQuery = SystemAPI.QueryBuilder().WithAll<ContextComponent>().Build();
            var context = contextQuery.GetSingleton<ContextComponent>();

            bool shouldFinish = false;

            foreach (RefRO<DeathEvent> deathEvent in SystemAPI.Query<RefRO<DeathEvent>>())
            {
                shouldFinish = deathEvent.ValueRO.Killed == characterEntity;
            }

            if (shouldFinish)
            {
                context.gameManager.FinishGame();
            }
        }
    }
}