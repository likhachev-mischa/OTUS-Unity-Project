using System;
using DI;
using Unity.Entities.Content;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Logic
{
    [Serializable]
    public sealed class SceneLoader : IInitializable
    {
        [SerializeField]
        private WeakObjectSceneReference sceneReference;

        public void Initialize()
        {
            sceneReference.LoadAsync(new ContentSceneParameters()
                { autoIntegrate = true, loadSceneMode = LoadSceneMode.Additive });
        }
    }
}