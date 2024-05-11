using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LoadSystem
{
    [CreateAssetMenu(menuName = "Configs/System/LoadingTasks/SceneUnloadingTask", fileName = "SceneUnloadingTask")]
    public sealed class SceneUnloadingTask : LoadingTask
    {
        [SerializeField]
        public int sceneId;

        public override async UniTask LoadTask()
        {
            await SceneManager.UnloadSceneAsync(sceneId);
        }
    }
}