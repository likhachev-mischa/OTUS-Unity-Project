using DI;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class DeathScreenAdapter : MonoBehaviour, IGameFinishListener
    {
        [SerializeField]
        private GameObject deathScreen;

        void IGameFinishListener.OnFinish()
        {
            deathScreen.SetActive(true);
        }
    }
}