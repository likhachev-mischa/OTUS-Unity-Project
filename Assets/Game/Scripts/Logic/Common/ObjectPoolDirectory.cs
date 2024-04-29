using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game.Logic.Common
{
    public sealed class ObjectPoolDirectory : IDisposable
    {
        private Dictionary<Type, ObjectPool<MonoBehaviour>> pools;
        private IObjectResolver objectResolver;

        public ObjectPoolDirectory(IObjectResolver objectResolver)
        {
            pools = new Dictionary<Type, ObjectPool<MonoBehaviour>>();
            this.objectResolver = objectResolver;
        }

        public T GetObject<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour
        {
            var type = typeof(T);
            if (!pools.ContainsKey(type))
            {
                pools.Add(type, new ObjectPool<MonoBehaviour>(objectResolver, prefab));
            }

            return pools[type].GetObject(position, rotation, parent) as T;
        }

        public void ReceiveObject<T>(T obj) where T : MonoBehaviour
        {
            var type = typeof(T);
            pools[type].ReceiveObject(obj);
        }

        public void Dispose()
        {
            foreach ((Type key, ObjectPool<MonoBehaviour> value) in pools)
            {
                value.Dispose();
            }

            pools.Clear();
        }
    }
}