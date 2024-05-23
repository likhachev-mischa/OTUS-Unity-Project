using Unity.Entities;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class EnemyViewAdapter : MonoBehaviour, IViewAdapter, IWeaponViewAdapterContainer
    {
        [field: SerializeField]
        public WeaponViewAdapter WeaponViewAdapter { get; private set; }

        public Entity Entity { get; set; }

        public void DrawVisuals(ComponentToDraw component, string data)
        {
        }

        public void DisableVisuals(ComponentToDraw component)
        {
        }
    }
}