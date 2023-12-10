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
    public const int FIRST_LEVEL_SCENE_BUILD_INDEX = 3;

    public const float NEW_PROGRESS_PLAYER_MONEY_AMOUNT = 0;

    public const int DEFAULT_MAX_STREAK_DAYS = 7;
    public const int DEFAULT_MAX_STREAK_REWARD = 1000;
    public const float DEFAULT_CLAIM_REWARD_COOLDOWN = 24f;
    public const float DEFAULT_REWARD_DEADLINE = 48f;
    #endregion

    #region SCENE NAMES
    public const string MAIN_MENU_SCENE_NAME = "MainMenu";
    public const string SHOP_SCENE_NAME = "Shop";
    public const string FIRST_LEVEL_NAME = "Level1";
    #endregion

    #region SHOP TAGS
    public const string SHOP_BLOCK_TICKETS_CONTENT_TAG = "TicketsContent";
    public const string SHOP_BLOCK_SKINS_CONTENT_TAG = "SkinsContent";
    public const string SHOP_BLOCK_LOCATIONS_CONTENT_TAG = "LocationsContent";
    #endregion
}
