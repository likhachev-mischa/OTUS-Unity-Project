using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DI;
using UnityEngine;

namespace LoadSystem
{
    public sealed class ApplicationLoader : MonoBehaviour
    {
        [SerializeField]
        private LoadingTasksConfig loadingTasksConfig;

        [SerializeField]
        private ProjectContext projectContext;


        private void Start()
        {
            projectContext.RegisterProject();
            projectContext.StartProject();

            LoadApplicationAsync().Forget();
        }

        private async UniTaskVoid LoadApplicationAsync()
        {
            IReadOnlyList<LoadingTask> taskList = loadingTasksConfig.LoadingTasks;

            foreach (LoadingTask task in taskList)
            {
                if (task != null)
                {
                    await task.LoadTask();
                }
            }

            Debug.Log("all tasks completed");

            LoadScene();
        }

        private void LoadScene()
        {
            projectContext.RegisterScene();
            projectContext.StartScene();
        }
    }
}