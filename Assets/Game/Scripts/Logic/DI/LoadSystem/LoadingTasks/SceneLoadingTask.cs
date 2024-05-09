using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.Entities.Content;
using Unity.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadSystem
{
    public sealed class SceneLoadingTask : LoadingTask
    {
        //[SerializeField]
        // private WeakObjectSceneReference sceneReference;
        private int id = 1;

        public override async UniTask LoadTask()
        {
            // var scene = sceneReference.LoadAsync(
            //     new ContentSceneParameters() { autoIntegrate = true, loadSceneMode = LoadSceneMode.Additive });
            // var utcs = new UniTaskCompletionSource();
            //
            // while (!scene.isLoaded)
            // {
            //     await Task.Delay(10);
            // }
            //
            // Debug.Log("scene loaded");
            // utcs.TrySetResult();
            await SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);
            Debug.Log("scene loaded");
        }
    }
}