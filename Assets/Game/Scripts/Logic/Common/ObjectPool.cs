using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game.Logic.Common
{
    public sealed class ObjectPool<T> : IDisposable where T : MonoBehaviour
    {
        private readonly IObjectResolver objectResolver;

        private readonly T prefab;

        private readonly List<T> activeObjects;
        private readonly List<T> inactiveObjects;

        public ObjectPool(IObjectResolver objectResolver, T prefab, int initialSize = 0)
        {
            
            this.objectResolver = objectResolver;
            activeObjects = new List<T>(initialSize);
            inactiveObjects = new List<T>(initialSize);

            for (int i = 0; i < initialSize; ++i)
            {
                T spawnedObject = objectResolver.CreateGameObjectInstance(prefab, Vector3.zero, Quaternion.identity);
                spawnedObject.gameObject.SetActive(false);
                inactiveObjects[i] = spawnedObject;
            }

            this.prefab = prefab;
        }

        public T GetObject(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (inactiveObjects.Count == 0)
            {
                T spawnedObject = objectResolver.CreateGameObjectInstance(prefab, position, rotation, parent);
                activeObjects.Add(spawnedObject);
                return spawnedObject;
            }
            T inactiveObject = inactiveObjects[0];

            var transform = inactiveObject.GetComponent<Transform>();
            transform.parent = parent;
            transform.position = position;
            transform.rotation = rotation;
            inactiveObject.gameObject.SetActive(true);

            activeObjects.Add(inactiveObject);
            inactiveObjects.Remove(inactiveObject);
            return inactiveObject;
        }

        public void ReceiveObject(T obj)
        {
            obj.gameObject.SetActive(false);

            activeObjects.Remove(obj);
            inactiveObjects.Add(obj);
        }

        public void Dispose()
        {
            for (var i = 0; i < activeObjects.Count; i++)
            {
                GameObject.Destroy(activeObjects[i]);
            }

            for (int i = 0; i < inactiveObjects.Count; i++)
            {
                GameObject.Destroy(inactiveObjects[i]);
            }

            activeObjects.Clear();
            inactiveObjects.Clear();
        }
    }
}