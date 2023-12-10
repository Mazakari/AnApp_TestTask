using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShopItemData
{
    public bool isLocked;
    public bool isEquipped;
}

[Serializable]
public struct LevelCellData
{
    public int number;
    public string sceneName;
    public bool locked;
}

[Serializable]
public class GameMetaData
{
    public int currentLevelBuildIndex;
    public float playerMoney;

    public int currentStreak;
    public int maxStreakReward;
    public int maxStreak;
    public float streakCooldown;

    public DateTime? lastClaimTime;

    public GameObject currentSkinPrefab;

    public List<LevelCellData> levels;
    public List<ShopItemData> skins;

    public bool musicToggle;
    public bool soundToggle;
   

    public GameMetaData(float initialMoney, int defaultStreakReward, int defaultMaxStreak, float sefaultStreakCooldown, int initialLevelBuildIndex, GameObject defaultSkinPrefab)
	{
        playerMoney = initialMoney;
        currentLevelBuildIndex = initialLevelBuildIndex;

        currentStreak = 0;
        maxStreakReward = defaultStreakReward;
        maxStreak = defaultMaxStreak;
        streakCooldown = sefaultStreakCooldown;

        currentSkinPrefab = defaultSkinPrefab;

        levels = new List<LevelCellData>();
        skins = new List<ShopItemData>();

        musicToggle = true;
        soundToggle = true;
    }
}
