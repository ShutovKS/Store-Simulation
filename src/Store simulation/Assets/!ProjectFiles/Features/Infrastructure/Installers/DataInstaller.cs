using Data.Scene;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class DataInstaller : MonoInstaller
    {
        [SerializeField] private GameplaySceneData gameplaySceneData;

        public override void InstallBindings()
        {
            BindSceneData();
        }

        private void BindSceneData()
        {
            Container.BindInstance(gameplaySceneData);
        }
    }
}