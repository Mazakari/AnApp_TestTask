using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemsStaticData", menuName = "StaticData/Shop/ShopItems")]
public class ShopItemsStaticData : ScriptableObject
{
    [SerializeField] private List<ShopItemSkinData> _skinsData;
    public List<ShopItemSkinData> SkinsData => _skinsData;
}
