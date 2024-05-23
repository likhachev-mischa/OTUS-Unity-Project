using Game.Components;
using Game.Utils;
using Unity.Entities;
using UnityEngine;

namespace Game.Authoring
{
    public sealed class EnemyAuthoring : MonoBehaviour
    {
        [SerializeField]
        public MovementAuthoringComponent movementAuthoringComponent;

        [SerializeField]
        public VisualProxyAuthoringComponent visualProxyAuthoringComponent;

        [SerializeField]
        public WeaponAuthoringComponent weaponAuthoringComponent;

        [SerializeField]
        public AttackAuthoringComponent attackAuthoringComponent;

        [SerializeField]
        public AttackTypeAuthoring attackTypeAuthoring;

        [SerializeField]
        public HealthAuthoringComponent healthAuthoringComponent;


        public sealed class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(entity);
                AddComponent(entity, new TeamComponent() { Value = Team.ENEMY });
                AddSharedComponent(entity,
                    new AttackCooldownShared() { Value = authoring.attackAuthoringComponent.Cooldown });

                AddComponent(entity, new Health() { Value = authoring.healthAuthoringComponent.Health });

                AddComponent<SpawnerIDComponent>(entity);

                this.BakeMovementComponent(entity, authoring.movementAuthoringComponent);
                this.BakeVisualProxyComponent(entity, authoring.visualProxyAuthoringComponent);
                this.BakeWeaponComponent(entity, authoring.weaponAuthoringComponent);
                this.BakeAttackTypeComponent(entity, authoring.attackTypeAuthoring);
                this.BakeAttackComponent(entity, authoring.attackAuthoringComponent);
            }
        }
    }
}