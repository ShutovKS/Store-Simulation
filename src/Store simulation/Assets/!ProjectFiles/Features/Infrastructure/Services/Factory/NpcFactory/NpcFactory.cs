using System.Collections.Generic;
using Data.Scene;
using Infrastructure.Services.AssetsAddressables;
using NonPlayerCharacter;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factory.NpcFactory
{
    public class NpcFactory : INpcFactory
    {
        public NpcFactory(DiContainer container, IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            _container = container;
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }

        private readonly DiContainer _container;
        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;

        private readonly Dictionary<NpcController, GameObject> _npsControllers = new();

        public async void Spawn(GameplaySceneData gameplaySceneData)
        {
            var prefab = await _assetsAddressablesProvider.GetAsset<GameObject>(AssetsAddressableConstants.NPC_PREFAB);

            var instantiate = _container.InstantiatePrefab(prefab);
            instantiate.transform.position = gameplaySceneData.NpcSpawnPoint;

            var npcController = new NpcController(instantiate, gameplaySceneData);

            _npsControllers.Add(npcController, instantiate);
        }

        public void Remove(NpcController npcController)
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
    }
}