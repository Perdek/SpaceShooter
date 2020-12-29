using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.WaitingRoom
{
    [System.Serializable]
    public class ButtonsPanel
    {
        #region MEMBERS

        [SerializeField] private Button readyButton = default;
        [SerializeField] private Button saveButton = default;
        [SerializeField] private Button exitButton = default;

        #endregion

        #region PROPERTIES

        private Button ReadyButton => readyButton;
        private Button SaveButton => saveButton;
        private Button ExitButton => exitButton;

        #endregion

        #region METHODS

        public void AddListenerToReadyButton(UnityAction onClick)
        {
            ReadyButton.onClick.AddListener(onClick);
        }

        public void AddListenerToSaveButton(UnityAction onClick)
        {
            SaveButton.onClick.AddListener(onClick);
        }

        public void AddListenerToExitButton(UnityAction onClick)
        {
            ExitButton.onClick.AddListener(onClick);
        }

        #endregion
    }
}
