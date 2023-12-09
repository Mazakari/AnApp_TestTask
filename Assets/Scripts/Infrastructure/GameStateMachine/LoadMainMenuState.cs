using UnityEngine;

public class LoadMainMenuState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly LoadingCurtain _curtain;
    private readonly IPersistentProgressService _progressService;
    private readonly ILevelService _cellsService;
    private readonly IDailyBonusService _dailyBonusService;

    public LoadMainMenuState(
        GameStateMachine gameStateMachine, 
        SceneLoader sceneLoader, 
        LoadingCurtain curtain, 
        IGameFactory gameFactory, 
        IPersistentProgressService progressService, 
        ILevelService cellsService,
        IDailyBonusService dailyBonusService)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _gameFactory = gameFactory;
        _curtain = curtain;
        _gameFactory = gameFactory;
        _progressService = progressService;
        _cellsService = cellsService;
        _dailyBonusService = dailyBonusService;
    }

    public void Enter(string sceneName)
    {
        Debug.Log("LoadMainMenuState");
        _curtain.Show();
        _gameFactory.Cleanup();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
        _curtain?.Hide();

    private void OnLoaded()
    {
        InitMainMenu();
        InitVolumeControl();
        InitServices();

        InformProgressReaders();
        _gameStateMachine.Enter<MainMenuState>();
    }

    

    private void InitMainMenu() => 
        _gameFactory.CreateMainMenulHud();

    private void InitVolumeControl()
    {
        VolumeControl vc = Object.FindObjectOfType<VolumeControl>();
        if (vc != null) return;

        _gameFactory.CreateVolumeControl();
    }
    private void InitServices()
    {
        try
        {
            InitLevelCells();
            ImitDailyBonusCells();
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void InitLevelCells() => 
        _cellsService.InitService(_progressService.Progress);
    private void ImitDailyBonusCells() =>
       _dailyBonusService.InitService(_progressService.Progress);
    private void InformProgressReaders()
    {
        foreach (ISavedProgress progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_progressService.Progress);
        }
    }
}
