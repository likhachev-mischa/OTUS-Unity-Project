using System;
using UnityEngine;

namespace DI
{
    public interface IObjectResolver : IDisposable
    {
        T CreateInstance<T>() where T : new();

        T CreateMonoBehaviourInstance<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null)
            where T : MonoBehaviour;

        GameObject CreateGameObjectInstance(GameObject prefab, Vector3 position, Quaternion rotation,
            Transform parent = null);

        void UnbindObject(GameObject obj);
        void UnbindObject<T>(T obj);
    }
}