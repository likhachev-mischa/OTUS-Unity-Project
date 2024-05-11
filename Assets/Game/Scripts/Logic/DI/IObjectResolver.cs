using System;
using UnityEngine;

namespace DI
{
    public interface IObjectResolver : IDisposable
    {
       T CreateInstance<T>() where T : new();

        T CreateGameObjectInstance<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour;
    }
}