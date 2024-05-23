using System.Collections.Generic;
using Game.Components;
using Game.Logic.Common;
using Game.Systems;
using Unity.Entities;
using UnityEngine;

namespace Game.Visuals.Systems
{
    [UpdateInGroup(typeof(DeathSystemGroup))]
    public partial class ViewCleanupOnDeathSystem : SystemBase
    {
        private Dictionary<uint, DummyData> dummyDataLookup = new();
        private uint indexCounter = 0;
        private ObjectPoolDirectory pool;

        protected override void OnCreate()
        {
            RequireForUpdate<DeathEvent>();
            RequireForUpdate<ObjectPoolComponent>();
        }

        protected override void OnUpdate()
        {
            var poolQuery = SystemAPI.QueryBuilder().WithAll<ObjectPoolComponent>().Build();
            pool = poolQuery.GetSingleton<ObjectPoolComponent>().Value;

            foreach (RefRO<DeathEvent> deathEvent in SystemAPI.Query<RefRO<DeathEvent>>())
            {
                Entity killed = deathEvent.ValueRO.Killed;
                if (!EntityManager.HasComponent(killed, typeof(VisualTransform))
                    || !EntityManager.HasComponent(killed, typeof(VisualProxyPrefab)))
                {
                    continue;
                }

                var prefab = EntityManager.GetComponentObject<VisualProxyPrefab>(killed).Value;
                var go = EntityManager.GetComponentObject<VisualTransform>(killed).Value.gameObject;

                if (deathEvent.ValueRO.Info == DeathInfo.REGULAR &&
                    go.TryGetComponent(out DummySpawnerOnDeath dummySpawnerOnDeath))
                {
                    var obj = pool.SpawnObject(dummySpawnerOnDeath.dummyPrefab.gameObject, go.transform.position,
                        go.transform.rotation);

                    dummyDataLookup.Add(indexCounter,
                        new DummyData() { obj = obj, prefab = dummySpawnerOnDeath.dummyPrefab.gameObject });

                    var dummy = obj.GetComponent<Dummy>();
                    dummy.Index = indexCounter;
                    ++indexCounter;

                    dummy.Disabled += OnDummyDeath;
                }

                pool.ReceiveObject(prefab, go);
            }
        }

        private void OnDummyDeath(uint dummyIndex)
        {
            DummyData data = dummyDataLookup[dummyIndex];

            dummyDataLookup.Remove(dummyIndex);
            if (dummyDataLookup.Count == 0)
            {
                indexCounter = 0;
            }

            pool.ReceiveObject(data.prefab, data.obj);
            data.obj.GetComponent<Dummy>().Disabled -= OnDummyDeath;
        }

        private struct DummyData
        {
            public GameObject prefab;
            public GameObject obj;
        }
    }
}