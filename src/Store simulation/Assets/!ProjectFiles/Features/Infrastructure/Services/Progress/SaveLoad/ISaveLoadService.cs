namespace Infrastructure.Services.Progress.SaveLoad
{
    public interface ISaveLoadService
    {
        public void SaveProgress();
        public PlayerProgress LoadProgress();
    }
}