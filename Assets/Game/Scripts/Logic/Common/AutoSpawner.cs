using DI;
using Game.Components;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Game.Logic.Common
{
    public sealed class AutoSpawner : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private SpawnerID spawnerID;

        [SerializeField]
        private float delay = 5;

        [SerializeField]
        private int minHealth;

        [SerializeField]
        private int maxHealth;

        [SerializeField]
        private Transform[] spawnPoints;

        [SerializeField]
        private uint seed = 1;

        private Random randomizer;

        private float innerDelay;

        private SpawnerController spawnerController;

        [Inject]
        private void Construct(SpawnerController spawnerController)
        {
            this.spawnerController = spawnerController;
            innerDelay = delay;
            randomizer = new Random(seed);
        }

        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            innerDelay -= deltaTime;
            if (innerDelay <= 0)
            {
                Spawn();
                innerDelay = delay;
            }
        }

        private void Spawn()
        {
            int index = randomizer.NextInt(0, spawnPoints.Length);
            int health = randomizer.NextInt(minHealth, maxHealth);

            spawnerController.SendSpawnRequest(spawnerID, spawnPoints[index].position, quaternion.identity, health);
        }
    }
}