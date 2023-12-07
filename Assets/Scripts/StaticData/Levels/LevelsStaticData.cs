using UnityEngine;

[CreateAssetMenu(fileName ="LevelsStaticData", menuName = "StaticData/Levels/LevelsData")]
public class LevelsStaticData : ScriptableObject
{
    [SerializeField] private LevelSettingStaticData[] _levelsData;
    public LevelSettingStaticData[] LevelsData => _levelsData;
}
