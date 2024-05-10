using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factory.UIFactory
{
    public class UIFactory : IUIFactory
    {
        public UIFactory(DiContainer container, IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            _container = container;
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }

        private readonly DiContainer _container;
        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;

        private readonly Dictionary<WindowID, GameObject> _screenTypeToInstanceMap = new();
        private readonly Dictionary<Type, Component> _screenTypeToComponentMap = new();

        public async Task<GameObject> CreateScreen(string assetAddress, WindowID windowId)
        {
            var screenPrefab = await _assetsAddressablesProvider.GetAsset<GameObject>(assetAddress);
            var screenObject = _container.InstantiatePrefab(screenPrefab);

            if (_screenTypeToInstanceMap.ContainsKey(windowId))
            {
                Debug.LogWarning(
                    $"A screen with WindowID {windowId} already exists. Replacing the existing screen object.");

                Object.Destroy(_screenTypeToInstanceMap[windowId]);

                _screenTypeToInstanceMap[windowId] = screenObject;
            }
            else
            {
                _screenTypeToInstanceMap.Add(windowId, screenObject);
            }

            return screenObject;
        }

        public Task<T> GetScreenComponent<T>(WindowID windowId) where T : Component
        {
            if (_screenTypeToInstanceMap.TryGetValue(windowId, out var screenObject))
            {
                var screenComponent = screenObject.GetComponent<T>();

                if (screenComponent == null)
                {
                    Debug.LogError($"Screen component of type {typeof(T)} not found");
                    return Task.FromResult<T>(null);
                }

                _screenTypeToComponentMap[typeof(T)] = screenComponent;
                return Task.FromResult(screenComponent);
            }

            Debug.LogError($"Screen with WindowID {windowId} not found");
            return Task.FromResult<T>(null);
        }

        public void DestroyScreen(WindowID windowId)
        {
            if (_screenTypeToInstanceMap.TryGetValue(windowId, out var screenObject))
            {
                _screenTypeToInstanceMap.Remove(windowId);
                Object.Destroy(screenObject);
            }
            else
            {
                Debug.LogError($"Screen with WindowID {windowId} not found");
            }
        }
    }
}