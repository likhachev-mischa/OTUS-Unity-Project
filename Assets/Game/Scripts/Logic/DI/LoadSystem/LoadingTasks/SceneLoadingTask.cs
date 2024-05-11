using Cysharp.Threading.Tasks;
using Unity.Entities.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadSystem
{
    [CreateAssetMenu(menuName = "Configs/System/LoadingTasks/SceneLoadingTask", fileName = "SceneLoadingTask")]
    public sealed class SceneLoadingTask : LoadingTask
    {
        //[SerializeField]
        //private WeakObjectSceneReference sceneReference;
        [SerializeField]
        private int sceneId = 1;

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
            await SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
            Debug.Log("scene loaded");
        }
    }
}