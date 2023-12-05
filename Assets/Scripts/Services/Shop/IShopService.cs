using System.Collections.Generic;

public interface IShopService : IService
{
    ShopItemDataSOList ShopItemDataSOList { get; }

    List<ShopItem> SkinItems { get; set; }

    void HideAllModelsByType(ShopItemType modelsType);
    void ShowEquippedItemModel(ShopItemType modelsType);
    void ShowItemModelByIndex(ShopItemType modelsType, int indexToShow);
    void ShowItemModelByName(ShopItemType modelsType, string itemModelName);
}