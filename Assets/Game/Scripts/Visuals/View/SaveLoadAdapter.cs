using System;
using DI;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Visuals
{
    public sealed class SaveLoadAdapter : MonoBehaviour
    {
        [SerializeField]
        private Button saveButton;

        [SerializeField]
        private Button loadButton;

        private SaveLoadManager saveLoadManager;

        [Inject]
        private void Construct(SaveLoadManager saveLoadManager)
        {
            this.saveLoadManager = saveLoadManager;
            saveButton.onClick.AddListener(Save);
            loadButton.onClick.AddListener(Load);
        }

        private void Save()
        {
            saveLoadManager.Save().Forget();
        }

        private void Load()
        {
            saveLoadManager.Load().Forget();
        }

        private void OnDestroy()
        {
            saveButton.onClick.RemoveListener(Save);
            loadButton.onClick.RemoveListener(Load);
        }
    }
}