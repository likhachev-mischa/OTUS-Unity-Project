using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game.Logic.Common
{
    public sealed class ObjectPoolDirectory : IDisposable
    {
        private Dictionary<GameObject, ObjectPool> pools = new();
        private IObjectResolver objectResolver;

        public ObjectPoolDirectory(IObjectResolver objectResolver)
        {
            this.objectResolver = objectResolver;
        }

        public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)

        {
            if (!pools.ContainsKey(prefab))
            {
                pools.Add(prefab, new ObjectPool(objectResolver, prefab));
            }

            return pools[prefab].GetObject(position, rotation, parent) as GameObject;
        }

        public GameObject SpawnObject(GameObject prefab, Transform parent = null)
        {
            if (!pools.ContainsKey(prefab))
            {
                pools.Add(prefab, new ObjectPool(objectResolver, prefab));
            }

            return pools[prefab].GetObject(Vector3.zero, Quaternion.identity, parent) as GameObject;
        }

        public void ReceiveObject(GameObject prefab, GameObject obj)
        {
            pools[prefab].ReceiveObject(obj);
        }

        public void Clear(GameObject prefab)
        {
            pools[prefab].Dispose();
        }

        public void Dispose()
        {
            foreach ((GameObject key, ObjectPool value) in pools)
            {
                value.Dispose();
            }

            pools.Clear();
        }
    }
}