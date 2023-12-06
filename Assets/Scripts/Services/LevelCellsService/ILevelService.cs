public interface ILevelService : IService
{
    LevelCell[] Levels { get; }

    void InitService();
    void UnlockNextLevel(string nextLevelName);
}