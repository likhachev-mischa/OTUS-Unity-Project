using Game.Components;
using Game.Logic;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(VisualProxyInitializationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public sealed partial class VisualCharacterRegistration : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<ContextComponent>();
            RequireForUpdate<CharacterTag>();
            RequireForUpdate<VisualTransform>();
            RequireForUpdate<VisualProxySpawnRequest>();
        }

        protected override void OnUpdate()
        {
            EntityQuery contextQuery = SystemAPI.QueryBuilder().WithAll<ContextComponent>().Build();
            var contextComponent = contextQuery.GetSingleton<ContextComponent>();

            EntityQuery characterQuery =
                SystemAPI.QueryBuilder().WithAll<VisualTransform>().WithAll<CharacterTag>().Build();
            var obj = characterQuery.GetSingleton<VisualTransform>();

            if (contextComponent.Context.HasService(typeof(VisualTransform)))
            {
                contextComponent.Context.RemoveService(typeof(VisualTransform));
            }

            contextComponent.Context.BindService(typeof(VisualTransform), obj);
            contextComponent.ObjectResolver.CreateInstance<CameraCharacterTracker>();
        }
    }
}