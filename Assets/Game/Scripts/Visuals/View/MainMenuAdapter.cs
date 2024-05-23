using DI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Visuals
{
    public sealed class MainMenuAdapter : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;

        private GameManager gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
            startButton.onClick.AddListener(OnStartButton);
        }

        private void OnStartButton()
        {
            gameManager.StartGame();
            this.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(OnStartButton);
        }
    }
}