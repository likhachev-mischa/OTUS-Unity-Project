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
            baker.AddComponent<MoveDirection>(entity);
            baker.AddComponent<RotationDirection>(entity);
            BlobAssetReference<float> initialMovementSpeedBlob =
                BlobUtils.CreateInitialComponent(movementComponent.MovementSpeed);
            baker.AddBlobAsset(ref initialMovementSpeedBlob, out _);
            baker.AddComponent(entity,
                new MoveSpeed { Value = movementComponent.MovementSpeed, InitialValue = initialMovementSpeedBlob });

            BlobAssetReference<float> initialRotationSpeedBlob =
                BlobUtils.CreateInitialComponent(movementComponent.RotationSpeed);
            baker.AddBlobAsset(ref initialRotationSpeedBlob, out _);
            baker.AddComponent(entity,
                new RotationSpeed()
                    { Value = movementComponent.RotationSpeed, InitialValue = initialRotationSpeedBlob });

            baker.AddComponent(entity, new MovementFlags() { CanMove = true });

            baker.AddComponent<MovementState>(entity);

            BlobAssetReference<Curve> speedUpCurveBlob =
                BlobUtils.CreateCurveComponent(movementComponent.AccelerationCurve, movementComponent.CurvePrecision);
            baker.AddBlobAsset(ref speedUpCurveBlob, out _);

            BlobAssetReference<float> speedUpTimeBlob =
                BlobUtils.CreateInitialComponent(movementComponent.AccelerationTime);
            baker.AddBlobAsset(ref speedUpTimeBlob, out _);

            baker.AddComponent(entity, new SpeedUpCurve() { Curve = speedUpCurveBlob, SpeedUpTime = speedUpTimeBlob });
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

            var cooldownBlob = BlobUtils.CreateInitialComponent(attackAuthoringComponent.Cooldown);
            baker.AddBlobAsset(ref cooldownBlob, out _);
            baker.AddComponent(entity,
                new AttackCooldown() { Value = attackAuthoringComponent.Cooldown, InitialValue = cooldownBlob });
        }

        public static void BakeWeaponStatsComponent(this IBaker baker, Entity entity,
            in WeaponStatsAuthoringComponent weaponStatsAuthoringComponent)
        {
            BlobAssetReference<float> initialRotationSpeedBlob =
                BlobUtils.CreateInitialComponent(weaponStatsAuthoringComponent.RotationSpeed);
            baker.AddBlobAsset(ref initialRotationSpeedBlob, out _);
            baker.AddComponent(entity,
                new RotationSpeed()
                    { Value = weaponStatsAuthoringComponent.RotationSpeed, InitialValue = initialRotationSpeedBlob });

            baker.AddComponent<RotationDirectionAngle>(entity);

            BlobAssetReference<float> lengthBlob =
                BlobUtils.CreateInitialComponent(weaponStatsAuthoringComponent.Length);
            baker.AddBlobAsset(ref lengthBlob, out _);
            baker.AddComponent(entity, new WeaponLength() { Value = lengthBlob });
            baker.AddComponent(entity,
                new WeaponFlagsComponent() { Value = weaponStatsAuthoringComponent.WeaponFlags });
        }
    }
}