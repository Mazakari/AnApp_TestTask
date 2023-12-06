using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        SetFpsTarget();

        // TO DO Unity bug with GetSceneByBuildIndex() Init scene names manualy
        _sceneLoader.GetBuildNamesFromBuildSettings();

        Debug.Log("BootstrapState");
        _sceneLoader.Load(Constants.INITIAL_SCENE_NAME, onLoaded: EnterLoadLevel);
    }

    private void EnterLoadLevel() =>
        _gameStateMachine.Enter<LoadProgressState>();

    public void Exit() {}

    private void RegisterServices()
    {
        _services.RegisterSingle<IInputService>(new InputService());
        _services.RegisterSingle<IAssets>(new AssetProvider());
        _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
        _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        _services.RegisterSingle<ILevelService>(new LevelService(_services.Single<IGameFactory>(), _sceneLoader));
        _services.RegisterSingle<IMetaResourcesService>(new MetaResources());
        _services.RegisterSingle<IShopService>(new ShopService());
        _services.RegisterSingle<ISkinsService>(new SkinsService());
    }

    // System Settings
    private void SetFpsTarget() =>
        Application.targetFrameRate = Constants.MOBILE_TARGET_FRAMERATE;
}
