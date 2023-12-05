using UnityEngine;

public partial class LevelCellsService : ILevelCellsService
{
    private LevelCell[] _levels;
    public LevelCell[] Levels => _levels;

    private LevelCellsData[] _levelsData;
    public LevelCellsData[] LevelsData => _levelsData;

    public string CurrentLevelName { get; private set; }
    public LevelCell Current { get; private set; }

    private int _levelsCount;

    private LevelsDataSO _levelsDataSO;

    private readonly IGameFactory _gameFactory;
    private readonly SceneLoader _sceneLoader;

    public LevelCellsService(IGameFactory gameFactory, SceneLoader sceneLoader)
    {
        _gameFactory = gameFactory;
        _sceneLoader = sceneLoader;
    }

    public void InitService()
    {
        _levelsDataSO = Resources.Load<LevelsDataSO>(Constants.LEVELS_DATA_SO_PATH);
        _levelsCount = _sceneLoader.GetLevelsCount();

        _levels = new LevelCell[_levelsCount];
        _levelsData = new LevelCellsData[_levelsCount];

        CreateLevels();

        InitLevels();
        CopyLevelsData();
    }

    public void SetCurrentCell()
    {
        CurrentLevelName = _sceneLoader.GetCurrentLevelName();
        Current = GetCellByName(CurrentLevelName);
    }

    public void UnlockNextLevel(string nextLevelName)
    {
        LevelCell nextLevel = GetCellByName(nextLevelName);
        nextLevel.UnlockLevel();
     
        UnlockNextLevelData(nextLevelName);
    }

    private LevelCell GetCellByName(string name)
    {
        LevelCell cell = _levels[0];

        for (int i = 0; i < _levels.Length; i++)
        {
            if (_levels[i].LevelSceneName.Equals(name))
            {
                cell = _levels[i];
            }
        }

        return cell;
    }

    private void InitLevels()
    {
        string name;
        int number;
        bool levelLocked;

        for (int i = 0; i < _levelsDataSO.LevelsData.Length; i++)
        {
            name = _levelsDataSO.LevelsData[i].LevelSceneName;
            number = i + 1;
            levelLocked = _levelsDataSO.LevelsData[i].LevelLocked;

            _levels[i].InitLevelCell(number, name, levelLocked);
        }
    }

    private void CreateLevels()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i] = _gameFactory.CreateLevelCell().GetComponent<LevelCell>();
        }
    }

    private void CopyLevelsData()
    {
        for (int i = 0; i < _levelsData.Length; i++)
        {
            _levelsData[i].number = _levels[i].LevelNumber;
            _levelsData[i].sceneName = _levels[i].LevelSceneName;
            _levelsData[i].locked = _levels[i].LevelLocked;
        }
    }
   
    private void UnlockNextLevelData(string nextLevelName)
    {
        int nextLevelIndex = GetLevelDataIndex(nextLevelName);

        if (nextLevelIndex >= -1)
        {
            _levelsData[nextLevelIndex].locked = false;
        }
    }

    private int GetLevelDataIndex(string completedLevelName)
    {
        int index = -1;
        for (int i = 0; i < _levelsData.Length; i++)
        {
            if (_levelsData[i].sceneName.Equals(completedLevelName))
            {
                index = i;
            }
        }

        return index;
    }
}
