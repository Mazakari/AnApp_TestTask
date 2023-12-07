public class Constants
{
    #region SYSTEM
    public const int MOBILE_TARGET_FRAMERATE = 60;
    public const int PC_TARGET_FRAMERATE = 120;
    #endregion

    #region AUDIO
    public const string MUSIC_VOLUME_PARAMETER = "MusicVolume";
    public const float DEFAULT_MUSIC_VOLUME = 0.5f;

    public const string SOUNDS_VOLUME_PARAMETER = "SoundsVolume";
    public const float DEFAULT_SOUNDS_VOLUME = 0.5f;
    #endregion

    #region NEW PROGRESS DATA
    public const string PROGRESS_KEY = "ProgressKey";
    public const string INITIAL_SCENE_NAME = "Initial";
    public const string NEW_PROGRESS_FIRST_LEVEL_SCENE_NAME = "Level1";
    public const int NEW_PROGRESS_PLAYER_MONEY_AMOUNT = 0;
    #endregion

    #region SCENE NAMES
    public const string MAIN_MENU_SCENE_NAME = "MainMenu";
    public const string SHOP_SCENE_NAME = "Shop";
    public const string FIRST_LEVEL_NAME = "Level1";
    #endregion

    #region SPAWN POINT TAGS
    public const string SHOP_ITEM_VIEW_POINT_TAG = "ShopItemViewPoint";
    #endregion

    #region SHOP TAGS
    public const string SKINS_TAB_TAG = "Skins Tab";
    #endregion
}
