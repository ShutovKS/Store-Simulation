using System;
using System.Collections.Generic;
using Infrastructure.Services.Sounds;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace UI.Scripts.Base
{
    public class BaseScreen : MonoBehaviour
    {
        [SerializeField] protected List<StateWindowButton> _buttons;

        protected IWindowService _windowService;

        protected ISoundService SoundService;

        public void Construct(IWindowService windowService, ISoundService soundService)
        {
            _windowService = windowService;
            SoundService = soundService;
        }

        protected virtual void Awake()
        {
            foreach (var stateWindowButton in _buttons)
            {
                stateWindowButton.Button.onClick.AddListener(() => OnButtonClicked(stateWindowButton));
            }
        }

        protected virtual void OnButtonClicked(StateWindowButton stateWindowButton)
        {
            switch (stateWindowButton.ButtonState)
            {
                case ButtonState.Open:
                    _windowService.Open(stateWindowButton.WindowID);
                    break;
                case ButtonState.Close:
                    _windowService.Close(stateWindowButton.WindowID);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void OnDestroy()
        {
            foreach (var stateWindowButton in _buttons)
            {
                stateWindowButton.Button.onClick.RemoveListener(() => OnButtonClicked(stateWindowButton));
            }
        }
    }
}