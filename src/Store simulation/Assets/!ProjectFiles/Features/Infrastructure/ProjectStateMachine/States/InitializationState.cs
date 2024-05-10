using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.Services.Progress;

namespace Infrastructure.ProjectStateMachine.States
{
    public class InitializationState : IState<GameBootstrap>, IEnterable
    {
        public GameBootstrap Initializer { get; }

        public InitializationState(GameBootstrap initializer)
        {
            Initializer = initializer;
        }

        public void OnEnter()
        {
            ChangeStateToLoading();
        }

        private void LoadLocalGame()
        {
            //_progressService.SetProgress(_saveLoadService.LoadProgress() ?? InitializeProgress());

            InitializeGame();
        }

        private void InitializeGame()
        {
            //GetLanguageForGame();

            //_assetsAddressablesProvider.Initialize();

            //_uiFactory.Initialize(_windowService);

            ChangeStateToLoading();
        }

        private void ChangeStateToLoading()
        {
            Initializer.StateMachine.SwitchState<ResourcesLoadingState>();
        }

        private void CreateNewProgressGooglePlay()
        {
            var newProgress = InitializeProgress();

            //_progressService.SetProgress(newProgress);

            InitializeGame();
        }

        private PlayerProgress InitializeProgress()
        {
            var newProgress = new PlayerProgress();

            return newProgress;
        }
    }
}