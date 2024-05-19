using DI;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class PlayerObjectInstaller : GameObjectInstaller
    {
        [SerializeField]
        [Listener]
        private PlayerViewAdapter playerViewAdapter;

        [SerializeField]
        private PlayerView playerView;
    }
}