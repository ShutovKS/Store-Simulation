using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.DataBase;
using Infrastructure.Services.Factory.NpcFactory;
using Infrastructure.Services.Factory.UIFactory;
using Infrastructure.Services.Input;
using Infrastructure.Services.Progress.Progress;
using Infrastructure.Services.Progress.SaveLoad;
using Infrastructure.Services.Sounds;
using Infrastructure.Services.Windows;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private PlayerInputActionReader playerInputActionReader;

        public override void InstallBindings()
        {
            BindAssetsAddressables();
            BindCoroutineRunner();
            BindDataBase();
            BindFactory();
            BindInput();
            BindProgress();
            BindSounds();
            BindWindow();
        }

        private void BindAssetsAddressables()
        {
            Container.BindInterfacesTo<AssetsAddressablesProvider>().AsSingle();
        }

        private void BindCoroutineRunner()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this);
        }

        private void BindDataBase()
        {
            Container.BindInterfacesTo<ReadyData>().AsSingle();
        }

        private void BindFactory()
        {
            Container.BindInterfacesTo<NpcFactory>().AsSingle();
            Container.BindInterfacesTo<UIFactory>().AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<PlayerInputActionReader>().FromInstance(playerInputActionReader).AsSingle();
        }

        private void BindProgress()
        {
            Container.BindInterfacesTo<CurrentProgressService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
        }

        private void BindSounds()
        {
            Container.BindInterfacesTo<SoundService>().AsSingle();
        }

        private void BindWindow()
        {
            Container.BindInterfacesTo<WindowService>().AsSingle();
        }
    }
}