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
public struct LevelCellsData
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

    public GameObject currentSkinPrefab;

    public List<LevelCellsData> levels;
    public List<ShopItemData> skins;

    public float musicVolume;
    public bool musicToggle;

    public float soundVolume;
    public bool soundToggle;

    public bool gameRated;

    public GameMetaData(int initialMoney, string initialLevel, GameObject defaultSkinPrefab)
	{
        playerMoney = initialMoney;
        nextLevel = initialLevel;

        currentSkinPrefab = defaultSkinPrefab;

        levels = new List<LevelCellsData>();
        skins = new List<ShopItemData>();

        musicVolume = 0.5f;
        musicToggle = true;

        soundVolume = 0.5f;
        soundToggle = true;
    }
}
