using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public sealed class ObjectResolver : IObjectResolver
    {
        private readonly ServiceLocator serviceLocator;
        private readonly List<IGameListener> listeners = new();
        private readonly GameManager manager;
        private readonly SceneContext context;

        public ObjectResolver(ServiceLocator serviceLocator, GameManager manager, SceneContext context)
        {
            this.serviceLocator = serviceLocator;
            this.manager = manager;
            this.context = context;
        }

        public T CreateInstance<T>() where T : new()
        {
            T instance = new();
            if (instance is IGameListener listener)
            {
                manager.AddListener(listener);
                listeners.Add(listener);
            }

            DependencyInjector.Inject(instance, serviceLocator);
            return instance;
        }

        public T CreateGameObjectInstance<T>(T prefab, Vector3 position, Quaternion rotation,
            Transform parentTransform = null) where T : MonoBehaviour
        {
            T instance = GameObject.Instantiate(prefab, position, rotation, parentTransform);
            if (instance.TryGetComponent(out GameObjectInstaller installer))
            {
                context.RegisterInstaller(installer);
            }

            return instance;
        }

        public GameObject CreateGameObjectInstance(GameObject prefab, Vector3 position, Quaternion rotation,
            Transform parentTransform = null)
        {
            GameObject instance = GameObject.Instantiate(prefab, position, rotation, parentTransform);
            if (instance.TryGetComponent(out GameObjectInstaller installer))
            {
                context.RegisterInstaller(installer);
            }

            return instance;
        }

        public void Dispose()
        {
            for (var i = 0; i < listeners.Count; i++)
            {
                manager.RemoveListener(listeners[i]);
            }

            listeners.Clear();
        }
    }
}