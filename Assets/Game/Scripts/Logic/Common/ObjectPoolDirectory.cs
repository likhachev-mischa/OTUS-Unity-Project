using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game.Logic.Common
{
    public sealed class ObjectPoolDirectory : IDisposable
    {
        private Dictionary<MonoBehaviour, ObjectPool<MonoBehaviour>> pools;
        private IObjectResolver objectResolver;

        public ObjectPoolDirectory(IObjectResolver objectResolver)
        {
            pools = new Dictionary<MonoBehaviour, ObjectPool<MonoBehaviour>>();
            this.objectResolver = objectResolver;
        }

        public T GetObject<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour
        {
            if (!pools.ContainsKey(prefab))
            {
                pools.Add(prefab, new ObjectPool<MonoBehaviour>(objectResolver, prefab));
            }

            return pools[prefab].GetObject(position, rotation, parent) as T;
        }

        public T GetObject<T>(T prefab, Transform parent = null) where T : MonoBehaviour
        {
            if (!pools.ContainsKey(prefab))
            {
                pools.Add(prefab, new ObjectPool<MonoBehaviour>(objectResolver, prefab));
            }

            return pools[prefab].GetObject(Vector3.zero, Quaternion.identity, parent) as T;
        }

        public void ReceiveObject<T>(T obj) where T : MonoBehaviour
        {
            pools[obj].ReceiveObject(obj);
        }

        public void Dispose()
        {
            foreach ((MonoBehaviour key, ObjectPool<MonoBehaviour> value) in pools)
            {
                value.Dispose();
            }

            pools.Clear();
        }
    }
}