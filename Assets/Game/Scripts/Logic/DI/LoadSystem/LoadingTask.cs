using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LoadSystem
{
    public abstract class LoadingTask : ScriptableObject
    {
        public abstract UniTask LoadTask();
    }
}