using UnityEngine;

public partial class LevelService : ILevelService
{
    private LevelCell[] _levels;
    public LevelCell[] Levels => _levels;

    private LevelCellData[] _levelsData;

    private int _levelsCount;

    private LevelsStaticData _levelsStaticData;

    private readonly IGameFactory _gameFactory;
    private readonly SceneLoader _sceneLoader;


    public LevelService(IGameFactory gameFactory, SceneLoader sceneLoader)
    {
        _gameFactory = gameFactory;
        _sceneLoader = sceneLoader;
    }

    public void InitService(PlayerProgress progress)
    {
        _levelsStaticData = _gameFactory.GetLevelsData();
        _levelsCount = _sceneLoader.GetLevelsCount();

        _levels = new LevelCell[_levelsCount];
        _levelsData = new LevelCellData[_levelsCount];
        InitLevelCellData(progress);

        SpawnLevelCells();
        InitLevelCells();
    }

    public void UnlockNextLevel(string currentLevelName)
    {
        try
        {
            string nextLevelName = _sceneLoader.GetNextLevelName(currentLevelName);
            LevelCell nextLevel = GetCellByName(nextLevelName);
            nextLevel.UnlockLevel();

            UnlockNextLevelData(nextLevelName);
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    public void UpdateLevelsProgress(PlayerProgress progress)
    {
        progress.gameData.levels.Clear();

        for (int i = 0; i < _levelsData.Length; i++)
        {
            progress.gameData.levels.Add(_levelsData[i]);
        }
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
                    break;
                }
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }

        return cell;
    }
    private void SpawnLevelCells()
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

    private void InitLevelCellData(PlayerProgress progress)
    {
        try
        {
            if(progress.gameData.levels.Count == 0)
            {
                for (int i = 0; i < _levelsData.Length; i++)
                {
                    _levelsData[i].sceneName = _levelsStaticData.LevelsData[i].LevelSceneName;
                    _levelsData[i].number = i + 1;
                    _levelsData[i].locked = _levelsStaticData.LevelsData[i].LevelLocked;
                }
            }
            else
            {
                for (int i = 0; i < _levelsData.Length; i++)
                {
                    _levelsData[i].sceneName = progress.gameData.levels[i].sceneName;
                    _levelsData[i].number = progress.gameData.levels[i].number;
                    _levelsData[i].locked = progress.gameData.levels[i].locked;
                }
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }


    private void InitLevelCells()
    {
        string name;
        int number;
        bool levelLocked;

        try
        {
            for (int i = 0; i < _levelsData.Length; i++)
            {
                name = _levelsData[i].sceneName;
                number = _levelsData[i].number;
                levelLocked = _levelsData[i].locked;

                _levels[i].InitLevelCell(number, name, levelLocked);
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
                    break;
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
