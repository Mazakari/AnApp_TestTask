using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCell : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private Button _levelButton;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private GameObject _lockedState;

    private int _levelNumber = 0;
    public int LevelNumber => _levelNumber;

    private string _levelSceneName = string.Empty;
    public string LevelSceneName => _levelSceneName;

    private bool _levelLocked = true;
    public bool LevelLocked => _levelLocked;
    
    public static event Action<string> OnLevelCellPress;

    public void UnlockLevel()
    {
        if (!_levelLocked) return;

        _levelLocked = false;

        LockedInUi(_levelLocked);
    }

    public void InitLevelCell(int levelNumber, string levelName, bool levelLocked) => 
        InitLevelData(levelNumber, levelName, levelLocked);

    public void LoadButtonLevel() =>
        OnLevelCellPress?.Invoke(_levelSceneName);

    private void InitLevelData(int levelNumber, string levelName, bool levelLocked)
    {
        _levelNumber = levelNumber;
        _levelSceneName = levelName;
        _levelLocked = levelLocked;

        _levelNumberText.text = _levelNumber.ToString();
        LockedInUi(_levelLocked);
    }

    private void LockedInUi(bool locked) =>
       _lockedState.SetActive(locked);
}
