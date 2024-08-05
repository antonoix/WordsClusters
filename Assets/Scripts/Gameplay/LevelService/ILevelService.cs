namespace Infrastructure.Services.LevelService
{
    public interface ILevelService
    {
        LevelConfig GetActualLevel();
        void PassCurrentLevel();
    }
}