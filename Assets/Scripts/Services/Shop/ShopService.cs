using System.Collections.Generic;
using UnityEngine;

public class ShopService : IShopService
{
    public ShopItemDataSOList ShopItemDataSOList { get; private set; }

    public List<ShopItem> SkinItems { get; set; } = new List<ShopItem>();

    public ShopService() => 
        ShopItemDataSOList = Resources.Load<ShopItemDataSOList>(AssetPath.SHOP_ITEMS_DATA_LIST_PATH);

    public void ShowItemModelByName(ShopItemType modelsType, string itemModelName)
    {
        List<ShopItem> modelsToHide = IdentifyItemsType(modelsType);

        ShowByName(itemModelName, modelsToHide);
    }

    public void ShowItemModelByIndex(ShopItemType modelsType, int indexToShow)
    {
        List<ShopItem> modelsToHide = IdentifyItemsType(modelsType);

        ShowByIndex(indexToShow, modelsToHide);
    }

    public void HideAllModelsByType(ShopItemType modelsType)
    {
        List<ShopItem> modelsToHide = IdentifyItemsType(modelsType);

        HideModelsCollection(modelsToHide);
    }

    public void ShowEquippedItemModel(ShopItemType modelsType)
    {
        List<ShopItem> modelsToHide = IdentifyItemsType(modelsType);
        ShowEquipped(modelsToHide);
    }

    private List<ShopItem> IdentifyItemsType(ShopItemType type)
    {
        List<ShopItem> modelsToHide = new();
        if (type == ShopItemType.Skin)
        {
            modelsToHide = SkinItems;
        }

        return modelsToHide;
    }

    private void HideModelsCollection(List<ShopItem> collection)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            collection[i].ItemModel.SetActive(false);
        }
    }

    private void ShowByIndex(int indexToShow, List<ShopItem> modelsToHide)
    {
        for (int i = 0; i < modelsToHide.Count; i++)
        {
            if (i == indexToShow)
            {
                modelsToHide[i].ItemModel.SetActive(true);
                continue;
            }

            modelsToHide[i].ItemModel.SetActive(false);
        }
    }

    private void ShowByName(string itemModelName, List<ShopItem> modelsToHide)
    {
        for (int i = 0; i < modelsToHide.Count; i++)
        {
            if (modelsToHide[i].ItemModel.name == itemModelName)
            {
                modelsToHide[i].ItemModel.SetActive(true);
                continue;
            }

            modelsToHide[i].ItemModel.SetActive(false);
        }
    }

    private void ShowEquipped(List<ShopItem> itemsToSearchEquipped)
    {
        for (int i = 0; i < itemsToSearchEquipped.Count; i++)
        {
            if (itemsToSearchEquipped[i].isEquipped)
            {
                itemsToSearchEquipped[i].ItemModel.SetActive(true);
                continue;
            }

            itemsToSearchEquipped[i].ItemModel.SetActive(false);
        }
    }
}
