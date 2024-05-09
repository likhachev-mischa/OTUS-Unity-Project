using Game.Components;
using Game.Utils;
using Unity.Entities;
using UnityEngine;

namespace Game.Authoring
{
    public sealed class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField]
        public MovementAuthoringComponent movementAuthoringComponent;

        [SerializeField]
        public VisualProxyAuthoringComponent visualProxyAuthoringComponent;

        [SerializeField]
        public WeaponAuthoringComponent weaponAuthoringComponent;

        [SerializeField]
        public AttackAuthoringComponent attackAuthoringComponent;

        //[SerializeReference]
        //public IAttackTypeAuthoring attackTypeAuthoring;

        public sealed class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent<CharacterTag>(entity);

                AddComponent(entity, new TeamComponent() { Value = Team.PLAYER });
                AddComponent<ControllableTag>(entity);
                AddSharedComponent(entity,
                    new AttackCooldownShared() { Value = authoring.attackAuthoringComponent.Cooldown });

                this.BakeMovementComponent(entity, authoring.movementAuthoringComponent);
                this.BakeVisualProxyComponent(entity, authoring.visualProxyAuthoringComponent);
                this.BakeWeaponComponent(entity, authoring.weaponAuthoringComponent);
               //aaaaaaaaa this.BakeAttackTypeComponent(entity, authoring.attackTypeAuthoring);
                this.BakeAttackComponent(entity, authoring.attackAuthoringComponent);
            }
        }
    }
}