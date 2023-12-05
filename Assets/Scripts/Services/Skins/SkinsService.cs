using UnityEngine;

public class SkinsService : ISkinsService 
{
    public GameObject DefaultSkinPrefab { get; private set; }
    public GameObject CurrentSkinPrefab { get; private set; }

    public SkinsService() => 
        InitService();

    public void InitService()
    {
        //ToDo Add defaultPlayer skin to load
        //DefaultSkinPrefab = Resources.Load<GameObject>(AssetPath.PLAYER_PREFAB_PATH);
        CurrentSkinPrefab = DefaultSkinPrefab;
    }

    public void SetCurrentSkinPrefab(GameObject prefab) =>
        CurrentSkinPrefab = prefab;
}
