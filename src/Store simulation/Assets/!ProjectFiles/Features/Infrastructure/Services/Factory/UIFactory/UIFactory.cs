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

            if (_screenTypeToInstanceMap.TryAdd(windowId, screenObject))
            {
                return screenObject;
            }

            Debug.LogWarning($"Экран с WindowID {windowId} уже существует. Замена существующего.");

            Object.Destroy(_screenTypeToInstanceMap[windowId]);

            _screenTypeToInstanceMap[windowId] = screenObject;

            return screenObject;
        }

        public Task<T> GetScreenComponent<T>(WindowID windowId) where T : Component
        {
            if (_screenTypeToInstanceMap.TryGetValue(windowId, out var screenObject))
            {
                var screenComponent = screenObject.GetComponent<T>();

                if (screenComponent == null)
                {
                    Debug.LogError($"Экранный компонент типа {typeof(T)} не найден");

                    return Task.FromResult<T>(null);
                }

                _screenTypeToComponentMap[typeof(T)] = screenComponent;

                return Task.FromResult(screenComponent);
            }

            Debug.LogError($"Экран с WindowID {windowId} не найден");
            return Task.FromResult<T>(null);
        }

        public void DestroyScreen(WindowID windowId)
        {
            if (_screenTypeToInstanceMap.Remove(windowId, out var screenObject))
            {
                Object.Destroy(screenObject);
            }
            else
            {
                Debug.LogError($"Экран с WindowID {windowId} не найден");
            }
        }
    }
}