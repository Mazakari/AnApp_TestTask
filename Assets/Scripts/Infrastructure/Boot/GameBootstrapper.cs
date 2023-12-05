using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

    private Game _game;

    private void Awake()
    {
        LoadingCurtain curtain = Instantiate(_loadingCurtainPrefab);

        _game = new Game(this, curtain);
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
