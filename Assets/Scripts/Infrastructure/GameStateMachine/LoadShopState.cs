using System.Collections.Generic;
using UnityEngine;

public class LoadShopState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IShopService _shopService;
    private IPersistentProgressService _progressService;
    private readonly LoadingCurtain _curtain;

    public LoadShopState(
        GameStateMachine gameStateMachine, 
        SceneLoader sceneLoader, 
        LoadingCurtain curtain, 
        IGameFactory gameFactory, 
        IShopService shopService, 
        IPersistentProgressService progressService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _shopService = shopService;
        _progressService = progressService;
    }

    public void Enter(string sceneName)
    {
        Debug.Log("LoadShopState");
        _curtain.Show();
        _gameFactory.Cleanup();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
        _curtain?.Hide();

    private void OnLoaded()
    {
        InitShopCanvas();
       
        SpawnShopItems();

        InitShopTabs();

        InformProgressReaders();

        _gameStateMachine.Enter<ShopState>();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_progressService.Progress);
        }
    }

    private void SpawnShopItems()
    {
        Transform skinsParent = GameObject.FindGameObjectWithTag(Constants.SKINS_TAB_TAG).GetComponent<ShopTab>().Content.transform;

        SpawnItem(skinsParent, _shopService.ShopItemDataSOList.SkinsData, _shopService.SkinItems);
    }

    private void SpawnItem(Transform parent, List<ShopItemSkinDataSO> itemsDataList, List<ShopItem> shopItemsCollection)
    {
        shopItemsCollection.Clear();

        ShopItem item;
        ShopItemSkinDataSO itemData;

        for (int i = 0; i < itemsDataList.Count; i++)
        {
            itemData = itemsDataList[i];
            item = _gameFactory.CreateShopItem(parent).GetComponent<ShopItem>();

            item.InitSkinItem(itemData);

            shopItemsCollection.Add(item);
        }
    }

    private void InitShopCanvas() =>
        _gameFactory.CreateShopHud();

    private void InitShopTabs()
    {
        ShopTab skinsTab = GameObject.FindGameObjectWithTag(Constants.SKINS_TAB_TAG).GetComponent<ShopTab>();

        Transform shopModelsViewParent = GameObject.FindGameObjectWithTag(Constants.SHOP_ITEM_VIEW_POINT_TAG).transform;
        SetSkinsToViewPoint(shopModelsViewParent, _shopService.SkinItems);

        skinsTab.Show();
    }

    private void SetSkinsToViewPoint(Transform parent, List<ShopItem> shopItemsCollection)
    {
        for (int i = 0; i < shopItemsCollection.Count; i++)
        {
            SetModelTransform(parent, shopItemsCollection[i]);
        }
    }

    private void SetModelTransform(Transform parent, ShopItem item)
    {
        SetItemModelParent(parent, item);
        ResetItemModelPosition(item);
        IncreaseItemModelLocalScale(item);
    }
    private void SetItemModelParent(Transform parent, ShopItem item) => 
        item.ItemModel.transform.SetParent(parent);

    private void ResetItemModelPosition(ShopItem item) => 
        item.ItemModel.transform.localPosition = Vector3.zero;

    private void IncreaseItemModelLocalScale(ShopItem item) => 
        item.ItemModel.transform.localScale = Vector3.one * 10f;
}