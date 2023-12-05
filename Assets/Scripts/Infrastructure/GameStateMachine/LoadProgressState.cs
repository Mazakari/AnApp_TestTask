using UnityEngine;

public class LoadProgressState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    private readonly ISkinsService _skinService;


    public LoadProgressState(
        GameStateMachine gameStateMachine, 
        IPersistentProgressService progressService, 
        ISaveLoadService saveLoadService, 
        ISkinsService skinService)
    {
        _gameStateMachine = gameStateMachine;
        _progressService = progressService;
        _saveLoadService = saveLoadService;
        _skinService = skinService;
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
        Debug.Log("Progress is null. Create new progress");
        return new(
            initialMoney: Constants.NEW_PROGRESS_PLAYER_MONEY_AMOUNT, 
            initialLevel: Constants.NEW_PROGRESS_FIRST_LEVEL_SCENE_NAME, 
            _skinService.DefaultSkinPrefab);
    }
}
