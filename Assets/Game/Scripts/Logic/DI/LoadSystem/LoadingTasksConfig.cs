using System.Collections.Generic;
using UnityEngine;

namespace LoadSystem
{
    [CreateAssetMenu(menuName = "Configs/System/LoadingTasks/Config", fileName = "LoadingTasksConfig", order = -1000)]
    public class LoadingTasksConfig : ScriptableObject
    {
        [SerializeField]
        private LoadingTask[] loadingTasks;

        public IReadOnlyList<LoadingTask> LoadingTasks
        {
            get { return loadingTasks; }
        }
    }
}