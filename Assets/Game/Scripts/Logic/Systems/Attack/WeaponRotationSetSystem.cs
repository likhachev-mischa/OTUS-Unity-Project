using Game.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Game.Systems
{
    [UpdateInGroup(typeof(AttackSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial struct WeaponRotationSetSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<AttackStartedEvent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityQuery query = SystemAPI.QueryBuilder().WithAll<AttackStartedEvent>().Build();
            ComponentLookup<LocalTransform> transformLookup = SystemAPI.GetComponentLookup<LocalTransform>();
            ComponentLookup<RotationDirectionAngle> rotationDirectionLookup =
                SystemAPI.GetComponentLookup<RotationDirectionAngle>();
            ComponentLookup<MeleeAttackAngle> attackAngleLookup = SystemAPI.GetComponentLookup<MeleeAttackAngle>();
            ComponentLookup<WeaponEntity> weaponLookup = SystemAPI.GetComponentLookup<WeaponEntity>();

            var weaponRotationSetJob = new WeaponRotationSetJob()
            {
                WeaponLookup = weaponLookup, TransformLookup = transformLookup, AttackAngleLookup = attackAngleLookup,
                RotationDirectionAngleLookup = rotationDirectionLookup
            };

            state.Dependency = weaponRotationSetJob.Schedule(query, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }

    [BurstCompile]
    public partial struct WeaponRotationSetJob : IJobEntity
    {
        [ReadOnly]
        public ComponentLookup<WeaponEntity> WeaponLookup;

        [ReadOnly]
        public ComponentLookup<MeleeAttackAngle> AttackAngleLookup;

        public ComponentLookup<LocalTransform> TransformLookup;
        public ComponentLookup<RotationDirectionAngle> RotationDirectionAngleLookup;

        private void Execute(in AttackStartedEvent attackStartedEvent)
        {
            Entity sourceEntity = attackStartedEvent.Source;
            if (!WeaponLookup.TryGetComponent(sourceEntity, out WeaponEntity weapon) ||
                !AttackAngleLookup.TryGetComponent(sourceEntity, out MeleeAttackAngle attackAngle))
            {
                return;
            }

            RefRW<LocalTransform> transform = TransformLookup.GetRefRW(weapon.Value);
            float angle = attackAngle.StartPosition.Value;
            transform.ValueRW.Rotation = quaternion.identity;
            transform.ValueRW = transform.ValueRW.RotateY(-angle * math.TORADIANS);

            if (!RotationDirectionAngleLookup.HasComponent(weapon.Value))
            {
                return;
            }

            RefRW<RotationDirectionAngle> rotationDirection = RotationDirectionAngleLookup.GetRefRW(weapon.Value);
            rotationDirection.ValueRW.Initial = attackAngle.StartPosition.Value * math.TORADIANS;
            rotationDirection.ValueRW.Destination =
                attackAngle.TraversalAngle.Value * math.TORADIANS + rotationDirection.ValueRO.Initial;
        }
    }
}