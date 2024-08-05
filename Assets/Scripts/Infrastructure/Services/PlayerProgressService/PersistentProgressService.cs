namespace Internal.Scripts.Infrastructure.Services.PlayerProgressService
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }

        public PersistentProgressService()
        {
            
        }
        
        public void InitNewProgress()
        {
            PlayerProgress = new PlayerProgress();
        }

    }
}