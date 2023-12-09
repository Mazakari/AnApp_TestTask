using System;
using UnityEngine;

public interface IDailyBonusService : IService
{
    int MaxStreakReward { get; }
    int CurrentStreak { get; set; }
    int MaxStreak { get; }
    float ClaimCooldown { get; }
    DateTime? LastClaimTime { get; set; }

    void InitService(PlayerProgress progress);
    void ParentCellAndResetScale(RectTransform parent);
    void UpdateRewardCellsData(bool rewardCooldownPassed);
}