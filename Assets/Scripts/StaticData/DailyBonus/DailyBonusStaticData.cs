using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyBonusStaticData", menuName = "StaticData/DailyBonus/DailyBonusSettings")]
public class DailyBonusStaticData : ScriptableObject
{
    [Header("Reward Collection Settings")]
    public int maxStreakReward;
    public int maxDaysStreak;

    public float claimCooldown;

    [Space(10)]
    [Header("Rewards List")]
    public List<DailyReward> rewards;
}
