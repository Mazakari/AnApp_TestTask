using UnityEngine;

public interface IAssets : IService
{
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Transform parent);
    GameObject Instantiate(GameObject prefab, Vector2 at);
}