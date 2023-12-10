using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemsStaticData", menuName = "StaticData/Shop/ShopItems")]
public class ShopStaticData : ScriptableObject
{
    public List<ShopItemStaticData> TicketsData;
    public List<ShopItemStaticData> SkinsData;
    public List<ShopItemStaticData> LevelsData;
}
