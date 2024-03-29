﻿public interface ILevelService : IService
{
    LevelCell[] Levels { get; }
    int CurrentLevelBuildIndex { get; }

    void InitService(PlayerProgress progress);
    void UnlockNextLevel(string currentLevelName);
    void UpdateLevelsProgress(PlayerProgress progress);
}