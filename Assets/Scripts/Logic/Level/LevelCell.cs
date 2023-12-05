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

    public void UnlockLevel() => 
        _levelLocked = false;

    public void InitLevelCell(int levelNumber, string levelName, bool levelLocked) => 
        InitLevelData(levelNumber, levelName, levelLocked);

    private void InitLevelData(int levelNumber, string levelName, bool levelLocked)
    {
        _levelNumber = levelNumber;
        _levelSceneName = levelName;
        _levelLocked = levelLocked;

        _levelNumberText.text = _levelNumber.ToString();
        _lockedState.SetActive(_levelLocked);
    }

    public void LoadButtonLevel() =>
        OnLevelCellPress?.Invoke(_levelSceneName);
}
