using System;
using UnityEngine;

public class ContinueGame : MonoBehaviour
{
    private string _nextlevelSceneName = string.Empty;
    private IPersistentProgressService _progressService;

    public static event Action<string> OnContinueGamePress;

    private void OnEnable()
    {
        _progressService = AllServices.Container.Single<IPersistentProgressService>();
        _nextlevelSceneName = _progressService.Progress.gameData.nextLevel;
    }

    public void ContinueGameButton() => 
        OnContinueGamePress?.Invoke(_nextlevelSceneName);
}
