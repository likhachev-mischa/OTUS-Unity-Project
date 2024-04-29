﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace DI
{
    public abstract class GameInstaller :
        IGameListenerProvider,
        IServiceProvider,
        IInjectProvider
    {
        public IEnumerable<IGameListener> ProvideListeners()
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (FieldInfo field in fields)
            {
                if (field.IsDefined(typeof(ListenerAttribute)) &&
                    field.GetValue(this) is IGameListener gameListener)
                {
                    yield return gameListener;
                }
            }
        }

        public IEnumerable<(Type, object)> ProvideServices()
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (FieldInfo field in fields)
            {
                var attribute = field.GetCustomAttribute<ServiceAttribute>();
                if (attribute != null)
                {
                    Type type = attribute.Contract;
                    object service = field.GetValue(this);
                    yield return (type, service);
                }
            }
        }

        public Dictionary<Type, List<object>> ProvideServiceCollections()
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            Dictionary<Type, List<object>> dict = new();

            foreach (FieldInfo field in fields)
            {
                var attribute = field.GetCustomAttribute<ServiceCollectionAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                var array = Array.CreateInstance(attribute.Contract, 0);
                Type type = array.GetType();

                object service = field.GetValue(this);
                if (dict.TryGetValue(type, out List<object> value))
                {
                    value.Add(service);
                }
                else
                {
                    dict.Add(type, new List<object>());
                    dict[type].Add(service);
                }
            }

            return dict;
        }

        public void Inject(ServiceLocator serviceLocator)
        {
            FieldInfo[] fields = GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (FieldInfo field in fields)
            {
                object target = field.GetValue(this);
                DependencyInjector.Inject(target, serviceLocator);
            }
        }
    }
}