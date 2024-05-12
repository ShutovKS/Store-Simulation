using Extension.StateMachineCore;
using Infrastructure.Services.Windows;

namespace Infrastructure.ProjectStateMachine.States
{
    public class ResourcesLoadingState : IState<GameBootstrap>, IEnterable
    {
        public ResourcesLoadingState(GameBootstrap initializer, IWindowService windowService)
        {
            _windowService = windowService;
            Initializer = initializer;
        }

        private readonly IWindowService _windowService;
        public GameBootstrap Initializer { get; }

        public async void OnEnter()
        {
            await _windowService.Open(WindowID.Loading);

            LoadResources();
            
            Initializer.StateMachine.SwitchState<GameMainMenuState>();
        }

        private void LoadResources()
        {
        }
    }
}