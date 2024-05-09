using Game.Components;
using Game.Utils;
using Unity.Entities;
using UnityEngine;
using BoxCollider = UnityEngine.BoxCollider;

namespace Game.Authoring
{
    public sealed class WeaponAuthoring : MonoBehaviour
    {
        [SerializeField]
        public DamageAuthoringComponent damageAuthoringComponent;

        [SerializeField]
        public WeaponStatsAuthoringComponent weaponStatsAuthoringComponent;

        [SerializeField]
        private VisualProxyAuthoringComponent visualProxyAuthoringComponent;


        public class Baker : Baker<WeaponAuthoring>
        {
            public override void Bake(WeaponAuthoring authoring)
            {
                Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);

                this.BakeWeaponStatsComponent(entity, authoring.weaponStatsAuthoringComponent);
                //this.BakeVisualProxyComponent(entity, authoring.visualProxyAuthoringComponent);
                AddComponentObject(entity,
                    new VisualProxyPrefab() { Value = authoring.visualProxyAuthoringComponent.ViewPrefab });


                AddComponent<CachedWeaponCollisionFilter>(entity);

                AddComponent(entity,
                    new Damage { Value = authoring.damageAuthoringComponent.Damage });

                var collider = authoring.GetComponent<BoxCollider>();
                collider.size = new Vector3(1, 0, authoring.weaponStatsAuthoringComponent.Length);
                collider.center = new Vector3(0, 0, (float)(authoring.weaponStatsAuthoringComponent.Length * 0.5));
            }
        }
    }
}