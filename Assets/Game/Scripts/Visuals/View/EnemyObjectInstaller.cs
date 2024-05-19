using DI;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class EnemyObjectInstaller : GameObjectInstaller
    {
        [SerializeField]
        private DummySpawnerOnDeath dummySpawnerOnDeath;
    }
}