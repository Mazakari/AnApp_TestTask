using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettingsStaticData", menuName = "StaticData/Levels/LevelSettings")]
public class LevelSettingStaticData : ScriptableObject
{
    [Header("Level Settings")]
    [SerializeField] private string _levelSceneName = string.Empty;
    public string LevelSceneName => _levelSceneName;

    [SerializeField] private bool _levelLocked = false;
    public bool LevelLocked => _levelLocked;

}
