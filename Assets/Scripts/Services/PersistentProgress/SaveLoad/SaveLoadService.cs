using UnityEngine;

public class SaveLoadService : ISaveLoadService
{
    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
        _progressService = progressService;
        _gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
        foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
        {
            progressWriter.UpdateProgress(_progressService.Progress);
        }

        string progress = _progressService.Progress.ToJson();
        PlayerPrefs.SetString(Constants.PROGRESS_KEY, progress);
    }

    public PlayerProgress LoadProgress()
    {
        string progressString = PlayerPrefs.GetString(Constants.PROGRESS_KEY);

        return GetPlayerProgress(progressString);
    }

    private PlayerProgress GetPlayerProgress(string progressString) =>
        DeserializeJProgressJSON(progressString);

    private PlayerProgress DeserializeJProgressJSON(string progressString) =>
        progressString.ToDeserialized<PlayerProgress>();
}
