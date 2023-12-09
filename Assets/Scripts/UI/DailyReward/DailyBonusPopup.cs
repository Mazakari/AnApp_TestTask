using System;
using System.Collections;
using UnityEngine;

public class DailyBonusPopup : MonoBehaviour
{
    public static event Action OnCloseDailyBonusPopup;

    [SerializeField] private RectTransform _rewardsGrid;

    private IDailyBonusService _dailyBonusService;

    private bool _rewardColldownPassed = false;

    private float _claimDeadline = 48f;

    private void OnEnable()
    {
        CacheServices();
        ParentRewardCellsToGrid();
    }

    private void Start() => 
        StartCoroutine(RewardsStateUpdater());


    public void ClosePopup()
    {
        //ToDo save rewards progress
        OnCloseDailyBonusPopup?.Invoke();
    }

    private void CacheServices() =>
         _dailyBonusService = AllServices.Container.Single<IDailyBonusService>();

    private void ParentRewardCellsToGrid() =>
        _dailyBonusService.ParentCellAndResetScale(_rewardsGrid);

    private IEnumerator RewardsStateUpdater()
    {
        while (true)
        {
            UpdateRewardsState();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateRewardsState()
    {
        _rewardColldownPassed = true;
      
        if (_dailyBonusService.LastClaimTime.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _dailyBonusService.LastClaimTime.Value;

            if (timeSpan.TotalHours > _claimDeadline)
            {
                _dailyBonusService.LastClaimTime = null;
                _dailyBonusService.CurrentStreak = 0;
            }
            else if(timeSpan.TotalHours < _dailyBonusService.ClaimCooldown)
            {
                _rewardColldownPassed = false;
            }
        }

        UpdateRewardsUI();
    }

    private void UpdateRewardsUI()
    {
        if (_rewardColldownPassed)
        {
            Debug.Log("Claim reward!");
        }
        else
        {
            var nextClaimTime = _dailyBonusService.LastClaimTime.Value.AddHours(_dailyBonusService.ClaimCooldown);
            var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;

            string cd = $"{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
            Debug.Log($"Come back in {cd} for your next reward");
        }

        _dailyBonusService.UpdateRewardCellsData(_rewardColldownPassed);
    }
}
