using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Visuals
{
    public sealed class AbilityUI : MonoBehaviour
    {
        [field: SerializeField]
        public ComponentToDraw ComponentToDraw { get; private set; }

        [SerializeField]
        private TextMeshProUGUI valueText;

        [SerializeField]
        private GameObject abilityDrawObject;

        public void Enable()
        {
            abilityDrawObject.SetActive(true);
        }

        public void Draw(string value)
        {
            valueText.text = value.Substring(0, math.min(3, value.Length - 1));
        }

        public void Disable()
        {
            abilityDrawObject.SetActive(false);
        }
    }
}