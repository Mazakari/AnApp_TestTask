using UnityEngine;

public class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly ISkinsService _skinService;
    private readonly IGameFactory _gameFactory;


    public LoadProgressState(
        GameStateMachine gameStateMachine, 
        IPersistentProgressService progressService, 
        ISaveLoadService saveLoadService, 
        ISkinsService skinService,
        IGameFactory gameFactory)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        _skinService = skinService;
        _gameFactory = gameFactory;
    }

    public void Enter()
    {
        Debug.Log("LoadProgressState");
        LoadProgressOrInitNew();

        _gameStateMachine.Enter<LoadMainMenuState, string>(Constants.MAIN_MENU_SCENE_NAME);
    }

    public void Exit()
    {
    }

    private void LoadProgressOrInitNew() =>
        _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
    private PlayerProgress NewProgress()
    {
        int defaultStreakReward = Constants.DEFAULT_MAX_STREAK_REWARD;
        int defaultMaxStreak = Constants.DEFAULT_MAX_STREAK_DAYS;
        float defaultClaimCooldown = Constants.DEFAULT_CLAIM_REWARD_COOLDOWN;

        DailyBonusStaticData data = _gameFactory.GetDailyBonusData();
        if (data != null)
        {
            defaultStreakReward = data.maxStreakReward;
            defaultMaxStreak = data.maxDaysStreak;
            defaultClaimCooldown = data.claimCooldown;
        }

        Debug.Log("Progress is null. Create new progress");
        return new PlayerProgress(
            initialMoney: Constants.NEW_PROGRESS_PLAYER_MONEY_AMOUNT,
            defaultStreakReward: defaultStreakReward,
            defaultMaxStreak: defaultMaxStreak,
            sefaultStreakCooldown: defaultClaimCooldown,
            initialLevelBuildIndex: Constants.FIRST_LEVEL_SCENE_BUILD_INDEX, 
            _skinService.DefaultSkinPrefab);
    }
}
