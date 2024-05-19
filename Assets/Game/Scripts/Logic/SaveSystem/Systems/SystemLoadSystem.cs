using Game.Components;
using SaveSystem.Components;
using Unity.Entities;

namespace SaveSystem.Systems
{
    [UpdateInGroup(typeof(SystemSaveLoadSystemGroup))]
    public partial class SystemLoadSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<SystemLoadEvent>();
            RequireForUpdate<ObjectPoolComponent>();
        }

        protected override void OnUpdate()
        {
            var eventQuery = SystemAPI.QueryBuilder().WithAll<SystemLoadEvent>().Build();
            EntityManager.DestroyEntity(eventQuery);

            var poolQuery = SystemAPI.QueryBuilder().WithAll<ObjectPoolComponent>().Build();
            var pool = poolQuery.GetSingleton<ObjectPoolComponent>();

            pool.Value.Dispose();
        }
    }
}