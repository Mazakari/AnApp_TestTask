using UnityEngine;

public interface IAssets : IService
{
    LevelsStaticData GetLevelsData(string path);
    ShopItemsStaticData GetShopItemsData(string path);
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Transform parent);
    GameObject Instantiate(GameObject prefab, Vector2 at);
}