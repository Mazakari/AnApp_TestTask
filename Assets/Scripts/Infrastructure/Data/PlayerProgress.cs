using System;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public GameMetaData gameData;

    public PlayerProgress(int initialMoney, int defaultStreakReward, int defaultMaxStreak, float sefaultStreakCooldown, string initialLevel, GameObject defaultPlayerSkinPrefab)
    {
        gameData = new GameMetaData(initialMoney, defaultStreakReward, defaultMaxStreak, sefaultStreakCooldown, initialLevel, defaultPlayerSkinPrefab);
    }
}
