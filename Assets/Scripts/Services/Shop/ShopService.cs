using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopService : IShopService
{
    public ShopStaticData ItemsStaticData { get; private set; }

    private List<ShopItem> _tickets = new();
    private List<ShopItem> _skins = new();
    private List<ShopItem> _locations = new();


    private readonly IGameFactory _gameFactory;

    public ShopService(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;

        CacheShopStaticData();
    }

    private void CacheShopStaticData() => 
        ItemsStaticData = _gameFactory.GetShopData();

    public void InitShopItems()
    {
        GetShopItems();
        LoadStaticDataToItems();
    }
    private void GetShopItems()
    {
        try
        {
            GameObject contentParent = GameObject.FindGameObjectWithTag(Constants.SHOP_BLOCK_TICKETS_CONTENT_TAG);
            _tickets = contentParent.GetComponentsInChildren<ShopItem>().ToList();

            contentParent = GameObject.FindGameObjectWithTag(Constants.SHOP_BLOCK_SKINS_CONTENT_TAG);
            _skins = contentParent.GetComponentsInChildren<ShopItem>().ToList();

            contentParent = GameObject.FindGameObjectWithTag(Constants.SHOP_BLOCK_LOCATIONS_CONTENT_TAG);
            _locations = contentParent.GetComponentsInChildren<ShopItem>().ToList();
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void LoadStaticDataToItems()
    {
        try
        {
            LoadShopItemDataFromStaticData(_tickets, ItemsStaticData.TicketsData);
            LoadShopItemDataFromStaticData(_skins, ItemsStaticData.SkinsData);
            LoadShopItemDataFromStaticData(_locations, ItemsStaticData.LevelsData);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void LoadShopItemDataFromStaticData(List<ShopItem> items, List<ShopItemStaticData> staticData)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].InitItemWithStaticData(staticData[i]);
        }
    }
}
