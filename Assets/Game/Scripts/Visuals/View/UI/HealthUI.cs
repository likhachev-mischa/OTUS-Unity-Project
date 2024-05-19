using UnityEngine;
using UnityEngine.UI;

namespace Game.Visuals
{
    public sealed class HealthUI : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        public void SetInitialHealth(string value)
        {
            slider.maxValue = float.Parse(value);
        }

        public void DrawHealth(string value)
        {
            slider.value = float.Parse(value);
        }
    }
}