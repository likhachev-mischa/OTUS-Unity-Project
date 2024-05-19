using System;
using System.Collections.Generic;
using DI;
using UnityEngine;

namespace Game.Logic.Common
{
    public sealed class ObjectPool : IDisposable
    {
        private readonly IObjectResolver objectResolver;

        private readonly GameObject prefab;

        private readonly List<GameObject> activeObjects;
        private readonly List<GameObject> inactiveObjects;

        public ObjectPool(IObjectResolver objectResolver, GameObject prefab, int initialSize = 0)
        {
            this.objectResolver = objectResolver;
            activeObjects = new List<GameObject>(initialSize);
            inactiveObjects = new List<GameObject>(initialSize);

            for (int i = 0; i < initialSize; ++i)
            {
                GameObject spawnedObject =
                    objectResolver.CreateGameObjectInstance(prefab, Vector3.zero, Quaternion.identity);
                spawnedObject.gameObject.SetActive(false);
                inactiveObjects[i] = spawnedObject;
            }

            this.prefab = prefab;
        }

        public GameObject GetObject(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (inactiveObjects.Count == 0)
            {
                GameObject spawnedObject = objectResolver.CreateGameObjectInstance(prefab, position, rotation, parent);
                activeObjects.Add(spawnedObject);
                return spawnedObject;
            }

            GameObject inactiveObject = inactiveObjects[0];

            var transform = inactiveObject.GetComponent<Transform>();
            transform.parent = parent;
            transform.position = position;
            transform.rotation = rotation;
            inactiveObject.gameObject.SetActive(true);

            activeObjects.Add(inactiveObject);
            inactiveObjects.Remove(inactiveObject);
            return inactiveObject;
        }

        public void ReceiveObject(GameObject obj)
        {
            obj.gameObject.SetActive(false);

            activeObjects.Remove(obj);
            inactiveObjects.Add(obj);
        }

        public void Dispose()
        {
            for (var i = 0; i < activeObjects.Count; i++)
            {
                Debug.Log($"disposing {activeObjects[i].name}");
                objectResolver.UnbindObject(activeObjects[i]);
                GameObject.Destroy(activeObjects[i]);
            }

            for (int i = 0; i < inactiveObjects.Count; i++)
            {
                Debug.Log($"disposing {inactiveObjects[i].name}");
                objectResolver.UnbindObject(inactiveObjects[i]);
                GameObject.Destroy(inactiveObjects[i]);
            }

            activeObjects.Clear();
            inactiveObjects.Clear();
        }
    }
}