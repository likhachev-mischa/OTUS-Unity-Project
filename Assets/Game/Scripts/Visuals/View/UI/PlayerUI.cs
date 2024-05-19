using UnityEngine;

namespace Game.Visuals
{
    public sealed class PlayerUI : MonoBehaviour
    {
        [field: SerializeField]
        public HealthUI HealthUI { get; private set; }

        [field: SerializeField]
        public AbilityUI[] AbilityUis { get; private set; }
    }
}