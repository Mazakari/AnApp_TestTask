using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeeklyRewardProgress : MonoBehaviour 
{
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TMP_Text _progressText;

    private IDailyBonusService _dailyBonusService;


    private void OnEnable()
    {
        CacheServices();
        SubscribeUICallbacks();

        UpdateProgressUI();
    }

    private void OnDisable() => 
        UnsubscribeUICallbacks();

    private void UpdateProgressUI()
    {
        try
        {
            int currentStreak = _dailyBonusService.CurrentStreak;
            int maxStreak = _dailyBonusService.MaxStreak;

            UpdateSlider(currentStreak, maxStreak);
            UpdateCounterText(currentStreak, maxStreak);
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
       
    }

    private void UpdateCounterText(int currentStreak, int maxStreak) => 
        _progressText.text = $"{currentStreak} / {maxStreak}";

    private void UpdateSlider(int currentStreak, int maxStreak)
    {
        _progressSlider.value = currentStreak;
        _progressSlider.maxValue = maxStreak;
    }

    private void CacheServices() =>
        _dailyBonusService = AllServices.Container.Single<IDailyBonusService>();

    private void SubscribeUICallbacks() =>
       DailyBonusCell.OnDailyBonusCollected += UpdateProgressUI;
    private void UnsubscribeUICallbacks() =>
       DailyBonusCell.OnDailyBonusCollected -= UpdateProgressUI;
}
