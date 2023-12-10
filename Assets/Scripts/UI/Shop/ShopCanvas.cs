using System;
using UnityEngine;

public class ShopCanvas : MonoBehaviour
{
    public static event Action OnMainMenuButton;

    private ISaveLoadService _saveLoadService;

    private void OnEnable() => 
        CacheServices();

    public void LoadMainMenu()
    {
        SaveProgress();
        SendLoadMainMEnuCallback();
    }

    private void SaveProgress() =>
       _saveLoadService.SaveProgress();
    private void SendLoadMainMEnuCallback() => 
        OnMainMenuButton?.Invoke();

    private void CacheServices() =>
       _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
}
