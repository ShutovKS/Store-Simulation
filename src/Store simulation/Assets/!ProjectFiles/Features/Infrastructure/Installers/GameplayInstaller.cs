using UI.Market.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [FormerlySerializedAs("shopUI")] [SerializeField] private MarketUI marketUI;
        
        public override void InstallBindings()
        {
        //     Container.BindInstance<ShopUI>(shopUI);
        }
    }
}