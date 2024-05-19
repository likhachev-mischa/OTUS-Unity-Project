using System;
using DI;
using UnityEngine;

namespace Game.Visuals
{
    public class Dummy : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float deathTimeout;

        public event Action<uint> Disabled;

        public uint Index { get; set; }

        private float innerTimeout;

        private void OnEnable()
        {
            innerTimeout = deathTimeout;
        }

        void IGameUpdateListener.OnUpdate(float deltaTime)
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            innerTimeout -= deltaTime;
            if (innerTimeout <= 0)
            {
                Disabled?.Invoke(Index);
                gameObject.SetActive(false);
                innerTimeout = deathTimeout;
            }
        }
    }
}