using UnityEngine;

[CreateAssetMenu(fileName ="LevelsData", menuName = "Levels/Levels Data", order = 2)]
public class LevelsDataSO : ScriptableObject
{
    [SerializeField] private LevelSettingSO[] _levelsData;
    public LevelSettingSO[] LevelsData => _levelsData;
}
