using System;
using UI.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuScreen.Scripts
{
    public class MainMenuScreen : BaseScreen
    {
        public event Action OnStartGameButtonClicked;
        public event Action OnTestButtonClicked;
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _testButton;
        
        private new void Awake()
        {
            _startGameButton.onClick.AddListener(() => OnStartGameButtonClicked?.Invoke());
            _testButton.onClick.AddListener(() => OnTestButtonClicked?.Invoke());
            
            base.Awake();
        }
        
        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(() => OnStartGameButtonClicked?.Invoke());
            _testButton.onClick.RemoveListener(() => OnTestButtonClicked?.Invoke());
        }
    }
}
