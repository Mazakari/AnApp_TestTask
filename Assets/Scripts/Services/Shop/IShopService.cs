public interface IShopService : IService
{
    ShopStaticData ItemsStaticData { get; }

    void InitShopItems();
}