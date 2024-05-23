using DI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Visuals
{
    public sealed class PauseResumeAdapter : MonoBehaviour
    {
        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private Button resumeButton;

        private GameManager gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            this.gameManager = gameManager;
            pauseButton.onClick.AddListener(OnPauseButton);
            resumeButton.onClick.AddListener(OnResumeButton);
        }

        private void OnPauseButton()
        {
            gameManager.PauseGame();
            resumeButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }

        private void OnResumeButton()
        {
            gameManager.ResumeGame();
            resumeButton.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            pauseButton.onClick.RemoveListener(OnPauseButton);
            resumeButton.onClick.RemoveListener(OnResumeButton);
        }
    }
}