public class InputService : IInputService
{
    public InputActions InputActions { get; private set; }

    public InputService()
    {
        InputActions = new InputActions();
        InputActions.Platform.Enable();
        InputActions.Ball.Enable();
    }
}
