using System.Collections.Generic;
using Data.Scene;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.Market;
using NonPlayerCharacter;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factory.NpcFactory
{
    public class NpcFactory : INpcFactory
    {
        public NpcFactory(DiContainer container, IAssetsAddressablesProvider assetsAddressablesProvider,
            IMarketService marketService, ICoroutineRunner coroutineRunner)
        {
            _container = container;
            _assetsAddressablesProvider = assetsAddressablesProvider;
            _marketService = marketService;
            _coroutineRunner = coroutineRunner;
        }

        private readonly DiContainer _container;
        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;
        private readonly IMarketService _marketService;
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly Dictionary<NpcController, GameObject> _npsControllers = new();

        public async void Spawn(GameplaySceneData gameplaySceneData)
        {
            var prefab = await _assetsAddressablesProvider.GetAsset<GameObject>(AssetsAddressableConstants.NPC_PREFAB);

            var instantiate = _container.InstantiatePrefab(prefab);
            instantiate.transform.position = gameplaySceneData.NpcSpawnPoint;

            var npcController = new NpcController(instantiate, gameplaySceneData, this,
                _coroutineRunner);

            _npsControllers.Add(npcController, instantiate);
        }

        public void ProductPurchase()
        {
            var cart = GenerateCart();
            _marketService.PurchaseByBuyer(cart);
        }

        public void DestroyNpc(NpcController npcController)
        {
            if (_npsControllers.Remove(npcController, out var instance))
            {
                Object.Destroy(instance);
            }
            else
            {
                Debug.LogError($"Npc не найден для уничтожения.");
            }
        }

        private (int, int)[] GenerateCart()
        {
            var productQuantity = Random.Range(1, 5);
            var cart = new (int, int)[productQuantity];

            for (var i = 0; i < productQuantity; i++)
            {
                var productId = _marketService.Products.GetRandomProduct().Id;
                var productCount = Random.Range(1, 10);
                cart[i] = (productId, productCount);
            }

            return cart;
        }
    }
}