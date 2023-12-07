using UnityEngine;

public class MainMenuState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    public MainMenuState(GameStateMachine gameStateMachine) => 
        _gameStateMachine = gameStateMachine;

    public void Enter()
    {
        Debug.Log("MainMenuState");
        SubscribeUICallbacks();
    }

    public void Exit() => 
        UnsubscribeUICallbacks();

   
    private void LoadShop() =>
       _gameStateMachine.Enter<LoadShopState, string>(Constants.SHOP_SCENE_NAME);

    private void SubscribeUICallbacks() => 
        MainMenuCanvas.OnShopButtonPress += LoadShop;
    private void UnsubscribeUICallbacks() => 
        MainMenuCanvas.OnShopButtonPress -= LoadShop;

}
