using System;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public GameMetaData gameData;

    public PlayerProgress(float initialMoney, int defaultStreakReward, int defaultMaxStreak, float sefaultStreakCooldown, int initialLevelBuildIndex, GameObject defaultPlayerSkinPrefab)
    {
        gameData = new GameMetaData(initialMoney, defaultStreakReward, defaultMaxStreak, sefaultStreakCooldown, initialLevelBuildIndex, defaultPlayerSkinPrefab);
    }
}
