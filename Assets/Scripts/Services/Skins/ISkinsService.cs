using UnityEngine;
public interface ISkinsService : IService
{
    GameObject DefaultSkinPrefab { get; }
    GameObject CurrentSkinPrefab { get; }

    void SetCurrentSkinPrefab(GameObject prefab);
}