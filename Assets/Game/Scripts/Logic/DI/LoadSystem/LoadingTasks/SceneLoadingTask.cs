using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadSystem
{
    [CreateAssetMenu(menuName = "Configs/System/LoadingTasks/SceneLoadingTask", fileName = "SceneLoadingTask")]
    public sealed class SceneLoadingTask : LoadingTask
    {
        [SerializeField]
        private int sceneId = 1;

        public override async UniTask LoadTask()
        {
            await SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
            Debug.Log("scene loaded");
        }
    }
}