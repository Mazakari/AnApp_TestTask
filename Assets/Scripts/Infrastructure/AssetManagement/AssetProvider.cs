using UnityEngine;

public class AssetProvider : IAssets
{
    public GameObject Instantiate(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return Object.Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Vector2 at) =>
        Object.Instantiate(prefab, at, Quaternion.identity);

    public ShopItemsStaticData GetShopItemsData(string path)
    {
        ShopItemsStaticData data = Resources.Load<ShopItemsStaticData>(path);
        return data;
    }

    public LevelsStaticData GetLevelsData(string path)
    {
        LevelsStaticData data = Resources.Load<LevelsStaticData>(path);
        return data;
    }

    public DailyBonusStaticData GetDailyBonusData(string path)
    {
        DailyBonusStaticData data = Resources.Load<DailyBonusStaticData>(path);
        return data;
    }
}