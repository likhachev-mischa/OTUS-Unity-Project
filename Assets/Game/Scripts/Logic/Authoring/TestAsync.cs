using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Authoring
{
    public class TestAsync : MonoBehaviour
    {
        [SerializeField]
        private bool flag;

        [Button]
        private async void Func()
        {
            Debug.Log("func");
            await InnerFunc();
            Debug.Log("func ended");
        }

        [Button]
        private async void Func2()
        {
            await InnerFunc2();
            Debug.Log("wait ended");
        }

        private UniTask InnerFunc2()
        {
            return new UniTask();
        }

        private async UniTask InnerFunc()
        {
            Debug.Log("inner func");
            await InnerFlag();
        }

        private async UniTask InnerFlag()
        {
            Debug.Log("inner flag");
            await UniTask.WaitUntil(InnerCheck);
        }

        private bool InnerCheck()
        {
            return flag;
        }
    }
}