using Game.Components;
using Game.Systems;
using Game.Visuals.Components;
using Unity.Collections;
using Unity.Entities;
using Debug = UnityEngine.Debug;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(AnimationSystemGroup))]
    public unsafe partial class ViewSyncSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<OwnerEntity>();
        }

        protected override void OnUpdate()
        {
            Enabled = false;
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<WeaponLength>().WithAll<OwnerEntity>()
                .Build();
            // query.SetChangedVersionFilter(typeof(WeaponLength));

            NativeArray<Entity> array = query.ToEntityArray(Allocator.Temp);
            // SystemAPI.QueryBuilder()

            foreach (Entity weaponEntity in array)
            {
                var ownerEntity = EntityManager.GetComponentData<OwnerEntity>(weaponEntity);
                if (!EntityManager.HasComponent<ViewAdapterComponent>(ownerEntity.Value))
                {
                    return;
                }

                var weaponLength = EntityManager.GetComponentData<WeaponLength>(weaponEntity);
                var viewAdapter = EntityManager.GetComponentObject<ViewAdapterComponent>(ownerEntity.Value);
                //    viewAdapter.Value.ViewData = new WeaponViewData() { weaponLength = &weaponLength.Value };
                // viewAdapter.Value.aboba = true;
                //  EntityManager.SetComponentData(ownerEntity.Value, viewAdapter);
                //Debug.Log($"data set ");
            }


            // Dependency.Complete();
            array.Dispose();

            //job.Run();
        }
    }

    public unsafe partial struct ViewSyncJob : IJobEntity
    {
        public EntityManager EntityManager;

        private void Execute(in OwnerEntity ownerEntity, in WeaponLength weaponLength)
        {
            if (!EntityManager.HasComponent<ViewAdapterComponent>(ownerEntity.Value))
            {
                return;
            }

            //var test = &weaponLength;
            var viewAdapter = EntityManager.GetComponentObject<ViewAdapterComponent>(ownerEntity.Value);
            // viewAdapter.Value.ViewData = new WeaponViewData() { weaponLength = weaponLength };
            //viewAdapter.Value.DataChanged.Invoke();
        }
    }
}