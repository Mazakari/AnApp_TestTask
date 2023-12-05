using System;
using UnityEngine;

[Serializable]
public class PlayerProgress
{
    public GameMetaData gameData;

    public PlayerProgress(int initialMoney, string initialLevel, GameObject defaultPlayerSkinPrefab)
    {
        gameData = new GameMetaData(initialMoney, initialLevel, defaultPlayerSkinPrefab);
    }
}
