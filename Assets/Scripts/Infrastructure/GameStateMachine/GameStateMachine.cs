using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
    {
        _states = new Dictionary<Type, IExitableState>
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
            [typeof(LoadProgressState)] = new LoadProgressState(
                this, 
                services.Single<IPersistentProgressService>(), 
                services.Single<ISaveLoadService>(), 
                services.Single<ISkinsService>(),
                services.Single<IGameFactory>()),
            [typeof(LoadMainMenuState)] = new LoadMainMenuState(
                this, 
                sceneLoader, 
                curtain, 
                services.Single<IGameFactory>(), 
                services.Single<IPersistentProgressService>(), 
                services.Single<ILevelService>(),
                services.Single<IDailyBonusService>()),
            [typeof(MainMenuState)] = new MainMenuState(this),
            [typeof(LoadShopState)] = new LoadShopState(
                this, 
                sceneLoader, 
                curtain, 
                services.Single<IGameFactory>(), 
                services.Single<IShopService>(), 
                services.Single<IPersistentProgressService>()),
            [typeof(ShopState)] = new ShopState(this),
        };

    }
    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        IPayloadedState<TPayload> state = ChangeState<TState>();
        state.Enter(payload);
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
        _states[typeof(TState)] as TState;

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _currentState?.Exit();

        TState state = GetState<TState>();
        _currentState = state;

        return state;
    }
}
