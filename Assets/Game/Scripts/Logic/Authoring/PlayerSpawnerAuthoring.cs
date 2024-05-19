using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Game.Authoring
{
    public sealed class PlayerSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField]
        private PlayerAuthoring playerAuthoring;

        [SerializeField]
        private SpawnerID spawnerId;

        private class PlayerSpawnerBaker : Baker<PlayerSpawnerAuthoring>
        {
            public override void Bake(PlayerSpawnerAuthoring authoring)
            {
                var spawner = GetEntity(TransformUsageFlags.None);
                var prefab = GetEntity(authoring.playerAuthoring, TransformUsageFlags.Dynamic);

                AddComponent(spawner, new SpawnerEntityPrefab() { Prefab = prefab, SpawnerId = authoring.spawnerId });
            }
        }
    }
}