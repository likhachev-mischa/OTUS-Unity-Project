using Game.Components;
using Game.Logic;
using Unity.Entities;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(VisualProxyInitializationSystemGroup))]
    public sealed partial class VisualCharacterRegistration : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<ContextComponent>();
            RequireForUpdate<CharacterTag>();
            RequireForUpdate<VisualTransform>();
        }

        protected override void OnUpdate()
        {
            Enabled = false;

            EntityQuery contextQuery = SystemAPI.QueryBuilder().WithAll<ContextComponent>().Build();
            var context = contextQuery.GetSingleton<ContextComponent>();

            EntityQuery characterQuery =
                SystemAPI.QueryBuilder().WithAll<VisualTransform>().WithAll<CharacterTag>().Build();
            var obj = characterQuery.GetSingleton<VisualTransform>();

            context.Context.BindService(typeof(VisualTransform), obj);
            context.ObjectResolver.CreateInstance<CameraCharacterTracker>();
        }
    }
}