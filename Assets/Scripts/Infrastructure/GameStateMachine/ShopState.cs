public class ShopState : IState
{
    private readonly GameStateMachine _gameStateMachine;

    public ShopState(GameStateMachine stateMachine) => 
        _gameStateMachine = stateMachine;

    public void Enter()
    {
        //Debug.Log("ShopState");
        ShopCanvas.OnMainMenuButton += LoadMainMenu;
    }

    public void Exit()
    {
        ShopCanvas.OnMainMenuButton -= LoadMainMenu;

        // Optimization Test
        System.GC.Collect();
    }

    private void LoadMainMenu() =>
        _gameStateMachine.Enter<LoadMainMenuState, string>(Constants.MAIN_MENU_SCENE_NAME);
}
