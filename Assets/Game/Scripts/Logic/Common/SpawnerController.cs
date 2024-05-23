using DI;
using Game.Components;
using Game.Logic;
using Game.Utils;
using Sirenix.OdinInspector;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Game.Logic.Common
{
    public sealed class SpawnerController : MonoBehaviour
    {
        private EntityManager entityManager;

        [Inject]
        private void Construct(WorldRegistry worldRegistry)
        {
            this.entityManager = worldRegistry.GetWorld(Worlds.MAIN).EntityManager;
        }

        [Button]
        public void SendSpawnRequest(SpawnerID spawnerId, int health)
        {
            float3 position = float3.zero;
            quaternion rotation = quaternion.identity;
            var request = entityManager.CreateEntity();
            entityManager.AddComponent<EnemySpawnRequest>(request);
            entityManager.SetComponentData(request,
                new EnemySpawnRequest()
                {
                    SpawnData = new EntitySpawnData(new LocalTransform() { Position = position, Rotation = rotation },
                        new Health() { Value = health },spawnerId)
                });
        }

        [Button]
        public void SendSpawnRequest(SpawnerID spawnerId, float3 position, quaternion rotation, int health)
        {
            var request = entityManager.CreateEntity();
            entityManager.AddComponent<EnemySpawnRequest>(request);
            entityManager.SetComponentData(request,
                new EnemySpawnRequest()
                {
                    SpawnData = new EntitySpawnData(new LocalTransform() { Position = position, Rotation = rotation },
                        new Health() { Value = health }, spawnerId)
                });
        }
    }
}