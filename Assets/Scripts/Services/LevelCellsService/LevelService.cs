using UnityEngine;

public partial class LevelService : ILevelService
{
    private LevelCell[] _levels;
    public LevelCell[] Levels => _levels;

    private LevelCellData[] _levelsData;

    private int _levelsCount;

    private LevelsDataSO _levelsDataSO;

    private readonly IGameFactory _gameFactory;
    private readonly SceneLoader _sceneLoader;

    public LevelService(IGameFactory gameFactory, SceneLoader sceneLoader)
    {
        _gameFactory = gameFactory;
        _sceneLoader = sceneLoader;
    }

    public void InitService()
    {
        _levelsDataSO = Resources.Load<LevelsDataSO>(Constants.LEVELS_DATA_SO_PATH);
        _levelsCount = _sceneLoader.GetLevelsCount();

        _levels = new LevelCell[_levelsCount];
        _levelsData = new LevelCellData[_levelsCount];

        CreateLevels();

        InitLevels();
        CopyLevelsData();
    }

    public void UnlockNextLevel(string nextLevelName)
    {
        try
        {
            LevelCell nextLevel = GetCellByName(nextLevelName);
            nextLevel.UnlockLevel();
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }

        UnlockNextLevelData(nextLevelName);
    }

    private LevelCell GetCellByName(string name)
    {
        LevelCell cell = _levels[0];

        try
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                if (_levels[i].LevelSceneName.Equals(name))
                {
                    cell = _levels[i];
                }
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }

        return cell;
    }

    private void InitLevels()
    {
        string name;
        int number;
        bool levelLocked;

        try
        {
            for (int i = 0; i < _levelsDataSO.LevelsData.Length; i++)
            {
                name = _levelsDataSO.LevelsData[i].LevelSceneName;
                number = i + 1;
                levelLocked = _levelsDataSO.LevelsData[i].LevelLocked;

                _levels[i].InitLevelCell(number, name, levelLocked);
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void CreateLevels()
    {
        try
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i] = _gameFactory.CreateLevelCell().GetComponent<LevelCell>();
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void CopyLevelsData()
    {
        try
        {
            for (int i = 0; i < _levelsData.Length; i++)
            {
                _levelsData[i].number = _levels[i].LevelNumber;
                _levelsData[i].sceneName = _levels[i].LevelSceneName;
                _levelsData[i].locked = _levels[i].LevelLocked;
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void UnlockNextLevelData(string nextLevelName)
    {
        try
        {
            int nextLevelIndex = GetLevelDataIndex(nextLevelName);

            if (nextLevelIndex >= -1)
            {
                _levelsData[nextLevelIndex].locked = false;
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private int GetLevelDataIndex(string completedLevelName)
    {
        int index = -1;

        try
        {
            for (int i = 0; i < _levelsData.Length; i++)
            {
                if (_levelsData[i].sceneName.Equals(completedLevelName))
                {
                    index = i;
                }
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }

        return index;
    }
}
