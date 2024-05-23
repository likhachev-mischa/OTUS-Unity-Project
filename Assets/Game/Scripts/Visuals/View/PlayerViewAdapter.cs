using System.Globalization;
using DI;
using Game.Components;
using Game.Utils;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class PlayerViewAdapter : MonoBehaviour, IViewAdapter, IWeaponViewAdapterContainer
    {
        [field: SerializeField]
        public WeaponViewAdapter WeaponViewAdapter { get; private set; }

        [SerializeField]
        private PlayerView playerView;

        public Entity Entity
        {
            get => entity;
            set
            {
                entity = value;
                var health = entity.GetComponent<Health>().Value.ToString();
                if (!playerView.IsInitialized)
                {
                    playerView.SetInitialHealth(health);
                }

                playerView.Draw(ComponentToDraw.HEALTH, health);
            }
        }

        private Entity entity;

        public void DrawVisuals(ComponentToDraw component, string data)
        {
            playerView.Draw(component, data);
        }

        public void DisableVisuals(ComponentToDraw component)
        {
            playerView.Disable(component);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = new Color(1, 0, 0, 0.3f);
            float length = 1;
            if (Entity.TryGetComponent(out WeaponEntity weaponEntity))
            {
                length = weaponEntity.Value.GetComponent<WeaponLength>().Value;
            }

            var angles = Entity.GetComponent<MeleeAttackAngle>();

            float angle = angles.TraversalAngle.Value;

            float delta = angles.StartPosition.Value + angle;
            delta *= -1;
            delta *= math.TORADIANS;
            Vector3 position = transform.position;
            Vector3 forward = transform.forward;
            float directionAngle = math.atan2(forward.z, forward.x);
            float resultAngle = directionAngle - delta;

            var direction = new Vector3(math.cos(resultAngle), 0, math.sin(resultAngle));

            Handles.DrawSolidArc(
                new Vector3(position.x, position.y + 1, position.z),
                transform.up, direction, angle, length);
        }
#endif
        public void OnUpdate(float deltaTime)
        {
        }
    }
}