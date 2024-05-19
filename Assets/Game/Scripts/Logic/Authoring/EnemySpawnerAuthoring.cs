using Game.Components;
using Unity.Entities;
using UnityEngine;

namespace Game.Authoring
{
    public sealed class EnemySpawnerAuthoring : MonoBehaviour
    {
        [SerializeField]
        private EnemyAuthoring enemyAuthoring;

        [SerializeField]
        private SpawnerID spawnerId;

        private class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var spawner = GetEntity(TransformUsageFlags.None);
                var prefab = GetEntity(authoring.enemyAuthoring, TransformUsageFlags.Dynamic);

                AddComponent(spawner, new SpawnerEntityPrefab() { Prefab = prefab, SpawnerId = authoring.spawnerId });
            }
        }
    }
}