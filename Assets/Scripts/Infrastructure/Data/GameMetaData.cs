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
    public string nextLevel;
    public int playerMoney;

    public int currentStreak;
    public int maxStreakReward;
    public int maxStreak;
    public float streakCooldown;

    public GameObject currentSkinPrefab;

    public List<LevelCellData> levels;
    public List<ShopItemData> skins;

    public bool musicToggle;
    public bool soundToggle;
   

    public GameMetaData(int initialMoney, int defaultStreakReward, int defaultMaxStreak, float sefaultStreakCooldown, string initialLevel, GameObject defaultSkinPrefab)
	{
        playerMoney = initialMoney;
        nextLevel = initialLevel;

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
