using Game.Authoring;
using Game.Components;
using Unity.Entities;

namespace Game.Utils
{
    public static class BakerAuthoringComponentsExtensions
    {
        public static void BakeMovementComponent(this IBaker baker, Entity entity,
            in MovementAuthoringComponent movementComponent)
        {
            baker.AddComponent<MovementDirection>(entity);
            baker.AddComponent<RotationDirection>(entity);
            baker.AddComponent(entity,
                new MovementSpeed { Value = movementComponent.MovementSpeed });
            baker.AddSharedComponent(entity, new MovementSpeedShared() { Value = movementComponent.MovementSpeed });

            baker.AddComponent(entity,
                new RotationSpeed()
                    { Value = movementComponent.RotationSpeed });

            baker.AddComponent(entity, new MovementFlags() { CanMove = true });

            baker.AddComponent<MovementState>(entity);

            BlobAssetReference<Curve> speedUpCurveBlob =
                BlobUtils.CreateCurveComponent(movementComponent.AccelerationCurve, movementComponent.CurvePrecision);
            baker.AddBlobAsset(ref speedUpCurveBlob, out _);

            baker.AddComponent(entity,
                new SpeedUpCurve() { Curve = speedUpCurveBlob, SpeedUpTime = movementComponent.AccelerationTime });
        }

        public static void BakeVisualProxyComponent(this IBaker baker, Entity entity,
            VisualProxyAuthoringComponent visualProxyAuthoringComponent)
        {
            var visualPrefab = new VisualProxyPrefab() { Value = visualProxyAuthoringComponent.ViewPrefab };
            baker.AddComponentObject(entity, visualPrefab);
            baker.AddComponent<VisualProxySpawnRequest>(entity);
        }

        public static void BakeWeaponComponent(this IBaker baker, Entity entity,
            WeaponAuthoringComponent weaponAuthoringComponent)
        {
            Entity weaponPrefab = baker.GetEntity(weaponAuthoringComponent.weapon.gameObject,
                TransformUsageFlags.Dynamic);

            baker.AddComponent(entity, new WeaponPrefab() { Value = weaponPrefab });
            baker.AddComponent<WeaponInitializationRequest>(entity);
        }

        public static void BakeAttackTypeComponent(this IBaker baker, Entity entity,
            IAttackTypeAuthoring attackTypeAuthoring)
        {
            attackTypeAuthoring.Bake(baker, entity);
        }

        public static void BakeAttackComponent(this IBaker baker, Entity entity,
            in AttackAuthoringComponent attackAuthoringComponent)
        {
            baker.AddComponent(entity,
                new AttackStates()
                    { CanAttack = attackAuthoringComponent.CanAttack, IsAttacking = false, IsOnCooldown = false });

            baker.AddComponent(entity,
                new AttackCooldown() { Value = attackAuthoringComponent.Cooldown });
        }

        public static void BakeWeaponStatsComponent(this IBaker baker, Entity entity,
            in WeaponStatsAuthoringComponent weaponStatsAuthoringComponent)
        {
            baker.AddComponent(entity,
                new RotationSpeed()
                    { Value = weaponStatsAuthoringComponent.RotationSpeed });

            baker.AddComponent<RotationDirectionAngle>(entity);
            baker.AddComponent(entity, new WeaponLength() { Value = weaponStatsAuthoringComponent.Length });
            baker.AddComponent(entity,
                new WeaponFlagsComponent() { Value = weaponStatsAuthoringComponent.WeaponFlags });
        }
    }
}