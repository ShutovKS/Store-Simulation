using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.Factory.UIFactory;
using Infrastructure.Services.Input;
using Infrastructure.Services.Progress.Progress;
using Infrastructure.Services.Progress.SaveLoad;
using Infrastructure.Services.SoundsService;
using Infrastructure.Services.WindowsService;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private PlayerInputActionReader playerInputActionReader;

        public override void InstallBindings()
        {
            BindCurrentProgressService();
            BindUIFactory();
            BindAssetsAddressableService();
            BindPlayerInputActionReader();
            BindWindowService();
            BindCoroutineRunnerService();
            BindSaveLoadService();
            BindSoundsService();
        }

        private void BindSoundsService()
        {
            Container.BindInterfacesTo<SoundService>().AsSingle();
        }

        private void BindSaveLoadService()
        {
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
        }

        private void BindCurrentProgressService()
        {
            Container.BindInterfacesTo<CurrentProgressService>().AsSingle();
        }

        private void BindWindowService()
        {
            Container.BindInterfacesTo<WindowService>().AsSingle();
        }

        private void BindAssetsAddressableService()
        {
            Container.BindInterfacesTo<AssetsAddressablesProvider>().AsSingle();
        }

        private void BindPlayerInputActionReader()
        {
            Container.Bind<PlayerInputActionReader>().FromInstance(playerInputActionReader).AsSingle();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindCoroutineRunnerService()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this);
        }
    }
}