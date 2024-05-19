using System;
using DI;
using Game.Visuals;
using UnityEngine;

namespace Game.Scripts.Visuals.Installers
{
    [Serializable]
    public sealed class UIInstaller : GameInstaller
    {
        [SerializeField]
        [Service(typeof(PlayerUI))]
        private PlayerUI playerUI;
    }
}