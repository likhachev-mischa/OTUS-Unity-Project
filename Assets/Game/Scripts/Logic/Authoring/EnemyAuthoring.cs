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
        public HealthAuthoringComponent healthAuthoringComponent;

        //[SerializeField]
        //public WeaponAuthoringComponent weaponAuthoringComponent;

        // [SerializeField]
        //public AttackAuthoringComponent attackAuthoringComponent;

        //[SerializeReference]
        //public IAttackTypeAuthoring attackTypeAuthoring;


        public sealed class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new TeamComponent() { Value = Team.ENEMY });

                this.BakeMovementComponent(entity, authoring.movementAuthoringComponent);
                this.BakeVisualProxyComponent(entity, authoring.visualProxyAuthoringComponent);
                // this.BakeWeaponComponent(entity, authoring.weaponAuthoringComponent);
            }
        }
    }
}