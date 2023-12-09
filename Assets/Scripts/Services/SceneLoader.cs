using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    private List<string> _buildIndexScenesNames = new();

    public SceneLoader(ICoroutineRunner coroutineRunner) =>
        _coroutineRunner = coroutineRunner;

    public void Load(string name, Action onLoaded = null) => 
        _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

    private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
    {
        int firstlevelIndex = Constants.FIRST_LEVEL_SCENE_BUILD_INDEX;

        if (SceneManager.GetActiveScene().name == nextScene &&
            SceneManager.GetActiveScene().buildIndex < firstlevelIndex)
        {
            onLoaded?.Invoke();
            Debug.Log("Same scene. Do nothing");
            yield break;
        }

        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

        while (!waitNextScene.isDone)
        {
            yield return null;
        }

        onLoaded?.Invoke();
    }

    public int GetLevelsCount()
    {
        int firstLevelIndex = GetFirstLevelIndex();
        int totalScenesCount = _buildIndexScenesNames.Count;

        return totalScenesCount - firstLevelIndex;
    }

    public string GetNextLevelName(string currentLevelName)
    {
        string nextLevelName = currentLevelName;

        for (int i = 0; i < _buildIndexScenesNames.Count; i++)
        {
            if (_buildIndexScenesNames[i].Equals(currentLevelName))
            {
                int nextLevelIndex = i + 1;
                if (nextLevelIndex < _buildIndexScenesNames.Count)
                {
                    nextLevelName = _buildIndexScenesNames[nextLevelIndex];
                    break;
                }
            }
        }

        return nextLevelName;
    }

    /// <summary>
    /// Get all scenes names from Build Settings
    /// </summary>
    public void GetBuildNamesFromBuildSettings()
    {
        int scenesCount = SceneManager.sceneCountInBuildSettings;
        string pathToScene;
        string sceneName;

        for (int i = 0; i < scenesCount; i++)
        {
            pathToScene = SceneUtility.GetScenePathByBuildIndex(i);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
            _buildIndexScenesNames.Add(sceneName);
        }
    }

    private int GetFirstLevelIndex()
    {
        for (int i = 0; i < _buildIndexScenesNames.Count; i++)
        {
            if (_buildIndexScenesNames[i].Equals(Constants.FIRST_LEVEL_NAME))
            {
                return i;
            }
        }

        return -1;
    }
}
