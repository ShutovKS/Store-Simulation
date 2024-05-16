using System;
using System.Threading.Tasks;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.Factory.UIFactory;
using UnityEngine;

namespace Infrastructure.Services.Windows
{
    public class WindowService : IWindowService
    {
        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        private readonly IUIFactory _uiFactory;

        public async Task Open(WindowID windowID)
        {
            await OpenWindow(windowID);
        }

        public async Task<T> OpenAndGetComponent<T>(WindowID windowID) where T : Component
        {
            await OpenWindow(windowID);

            var component = _uiFactory.GetScreenComponent<T>(windowID).Result;

            return component;
        }

        private async Task OpenWindow(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.Unknown:
                    Debug.Log("Unknown window id: " + windowID);
                    break;
                case WindowID.None:
                    break;
                case WindowID.Loading:
                    await _uiFactory.CreateScreen(AssetsAddressableConstants.LOADING_SCREEN, windowID);
                    break;
                case WindowID.MainMenu:
                    await _uiFactory.CreateScreen(AssetsAddressableConstants.MAIN_MENU_SCREEN, windowID);
                    break;
                case WindowID.Gameplay:
                    await _uiFactory.CreateScreen(AssetsAddressableConstants.GAMEPLAY_SCREEN, windowID);
                    break;
                case WindowID.Manager:
                    await _uiFactory.CreateScreen(AssetsAddressableConstants.MANAGER_SCREEN, windowID);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowID), windowID, null);
            }
        }


        public void Close(WindowID windowID)
        {
            switch (windowID)
            {
                case WindowID.Loading:
                case WindowID.MainMenu:
                case WindowID.Gameplay:
                case WindowID.Manager:
                    _uiFactory.DestroyScreen(windowID);
                    break;
                default:
                    Debug.Log("Unknown window id + " + windowID);
                    break;
            }
        }
    }
}