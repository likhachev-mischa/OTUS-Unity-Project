using System;
using Game.Components;
using Game.Utils;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class EntityViewAdapter : MonoBehaviour, IViewAdapter
    {
        [SerializeField]
        public WeaponViewAdapter WeaponViewAdapter;

        public Entity Entity { get; set; }
        public event Action VisualsDrawn;

        public void DrawVisuals()
        {
            VisualsDrawn?.Invoke();
        }

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
            //delta -= delta;
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
    }
}