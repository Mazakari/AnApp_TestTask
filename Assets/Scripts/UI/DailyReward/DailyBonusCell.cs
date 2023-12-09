using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusCell : MonoBehaviour
{
    public static event Action OnDailyBonusCollected;

    [SerializeField] private Button _rewardButton;
    private bool _canClaim = false; 

    [SerializeField] private Image _background;
    [SerializeField] private Color _defaultColor; 
    [SerializeField] private Color _currentStreakColor; 

    [Space(10)]
    [SerializeField] private TMP_Text _dayNumberText;
    [SerializeField] private TMP_Text _rewardValueText;

    [Space(10)]
    [SerializeField] private Image _rewardImage;

    private DailyReward _cellReward;
    private IDailyBonusService _dailyBonusService;
    private IMetaResourcesService _metaResourcesService;

    private void OnEnable() => 
        CacheServices();

    public void ClaimReward()
    {
        if (!_canClaim) return;

        try
        {
            UpdateClaimTimeAndCurrentStreak();
            AddBonusReward();
            SendBonusCollectedCallback();
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }

    }
    public void InitCell(DailyReward reward)
    {
        try
        {
            CacheCellReward(reward);

            HandleCellClaimRewardButtonInteractableState(false);

            SetCellUIData();
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    public void SetRewardData(int day, int currentStreak, bool cooldownPassed)
    {
        try
        {
            HandleClaimRewardButtonState(day, currentStreak, cooldownPassed);

            SetCurrentStreakColor(day, currentStreak);
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    private void HandleClaimRewardButtonState(int day, int currentStreak, bool cooldownPassed)
    {
        if (StreakSameAsCellAndRewardClaimable(day, currentStreak, cooldownPassed))
        {
            HandleCellClaimRewardButtonInteractableState(true);
        }
        else if (currentStreak != day && _canClaim == true)
        {
            HandleCellClaimRewardButtonInteractableState(false);
        }
    }
    private void HandleCellClaimRewardButtonInteractableState(bool interactable)
    {
        _canClaim = interactable;
        _rewardButton.interactable = _canClaim;
    }
    private bool StreakSameAsCellAndRewardClaimable(int day, int currentStreak, bool cooldownPassed) =>
        currentStreak == day && _canClaim == false && cooldownPassed;

    private void CacheCellReward(DailyReward reward) =>
     _cellReward = reward;
    private void SetCellUIData()
    {
        _dayNumberText.text = $"Day {_cellReward.dayNumber}";
        _rewardValueText.text = _cellReward.value.ToString();

        _background.color = _defaultColor;

        _rewardImage.sprite = _cellReward.sprite;
    }
    private void SetCurrentStreakColor(int day, int currentStreak) =>
       _background.color = day == currentStreak ? _currentStreakColor : _defaultColor;
    private void UpdateClaimTimeAndCurrentStreak()
    {
        UpdateStreak();
        SaveStreakProgress();
    }

    private void UpdateStreak()
    {
        _dailyBonusService.LastClaimTime = DateTime.UtcNow;
        _dailyBonusService.CurrentStreak = (_dailyBonusService.CurrentStreak + 1) % _dailyBonusService.MaxStreak;
    }
    private void SaveStreakProgress() => 
        _dailyBonusService.SaveStreakData();

    private void AddBonusReward() => 
        _metaResourcesService.PlayerMoney += _cellReward.value;
    private static void SendBonusCollectedCallback() =>
       OnDailyBonusCollected?.Invoke();
    private void CacheServices()
    {
        _dailyBonusService = AllServices.Container.Single<IDailyBonusService>();
        _metaResourcesService = AllServices.Container.Single<IMetaResourcesService>();
    }
}
