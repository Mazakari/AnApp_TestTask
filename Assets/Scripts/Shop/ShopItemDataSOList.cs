using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemDataListSO", menuName = "Shop/ShopItemDataList", order = 2)]
public class ShopItemDataSOList : ScriptableObject
{
    [SerializeField] private List<ShopItemSkinDataSO> _skinsData;
    public List<ShopItemSkinDataSO> SkinsData => _skinsData;
}
