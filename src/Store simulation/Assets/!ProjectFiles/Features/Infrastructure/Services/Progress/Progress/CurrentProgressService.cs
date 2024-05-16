namespace Infrastructure.Services.Progress.Progress
{
    public class CurrentProgressService : IProgressService
    {
        public PlayerProgress PlayerProgress { get; private set; }

        public void SetProgress(PlayerProgress playerProgress)
        {
            PlayerProgress = playerProgress;
        }
    }
}