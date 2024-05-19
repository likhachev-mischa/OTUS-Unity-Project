using DI;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class DummyObjectInstaller : GameObjectInstaller
    {
        [SerializeField]
        [Listener]
        private Dummy dummy;
    }
}