using System;
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

    private void UnsubscribeUICallbacks()
    {
        LevelCell.OnLevelCellPress -= StartGame;
        ContinueGame.OnContinueGamePress -= StartGame;
        MainMenuCanvas.OnShopButtonPress -= LoadShop;
    }

    private void StartGame(string levelName)
    {
        // ToDo Unlock next level
        //_gameStateMachine.Enter<LoadLevelState, string>(levelName);
    }

    private void LoadShop() =>
       _gameStateMachine.Enter<LoadShopState, string>(Constants.SHOP_SCENE_NAME);

    private void SubscribeUICallbacks()
    {
        LevelCell.OnLevelCellPress += StartGame;
        ContinueGame.OnContinueGamePress += StartGame;
        MainMenuCanvas.OnShopButtonPress += LoadShop;
    }
}
