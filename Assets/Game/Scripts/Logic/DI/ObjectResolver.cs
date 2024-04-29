using System;
using System.Collections.Generic;
using UnityEngine;

namespace DI
{
    public sealed class ObjectResolver : IObjectResolver
    {
        private readonly ServiceLocator serviceLocator;
        private readonly List<Type> services;
        private readonly GameManager manager;

        public ObjectResolver(ServiceLocator serviceLocator, GameManager manager)
        {
            this.serviceLocator = serviceLocator;
            this.manager = manager;
            services = new List<Type>();
        }

        public T CreateInstance<T>() where T : new()
        {
            T instance = new();
            DependencyInjector.Inject(instance, serviceLocator);
            if (instance is IGameListener listener)
            {
                manager.AddListener(listener);
            }

            services.Add(typeof(T));
            return instance;
        }

        public T CreateGameObjectInstance<T>(T prefab, Vector3 position, Quaternion rotation,
            Transform parentTransform = null) where T : MonoBehaviour
        {
            T instance = GameObject.Instantiate(prefab, position, rotation, parentTransform);
            DependencyInjector.Inject(instance, serviceLocator);
            if (instance is IGameListener listener)
            {
                manager.AddListener(listener);
            }

            services.Add(typeof(T));
            return instance;
        }

        public void Dispose()
        {
            for (var i = 0; i < services.Count; i++)
            {
                serviceLocator.RemoveService(services[i]);
            }

            services.Clear();
        }
    }
}