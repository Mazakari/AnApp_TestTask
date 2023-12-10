using UnityEngine;

public class LoadShopState : IPayloadedState<string>
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly IGameFactory _gameFactory;
    private readonly IShopService _shopService;
    private readonly IPersistentProgressService _progressService;
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
        InformProgressReaders();

        _gameStateMachine.Enter<ShopState>();
    }

    private void InitShopCanvas()
    {
        _gameFactory.CreateShopHud();
        _shopService.InitShopItems();
    }

    private void InformProgressReaders()
    {
        foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
        {
            progressReader.LoadProgress(_progressService.Progress);
        }
    }
}