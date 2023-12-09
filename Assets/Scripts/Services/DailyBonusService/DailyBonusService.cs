using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusService : IDailyBonusService
{
    public int MaxStreakReward { get; private set; } = 0;
    public int CurrentStreak { get; set; } = 0;
    public int MaxStreak { get; private set; } = 0;

    public DateTime? LastClaimTime { get; set; }

    public float ClaimCooldown { get; private set; }

    private DailyBonusStaticData _dailyBonusStaticData;

    private List<DailyBonusCell> _dailyBonusCells;

    private readonly IGameFactory _gameFactory;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IPersistentProgressService _progressService;

    public DailyBonusService(IGameFactory gameFactory, ISaveLoadService saveLoadService, IPersistentProgressService progressService)
    {
        _gameFactory = gameFactory;
        _saveLoadService = saveLoadService;
        _progressService = progressService;
    }

    public void InitService(PlayerProgress progress)
    {
        _dailyBonusStaticData = _gameFactory.GetDailyBonusData();

        InitStreakValues(progress);
        
        SpawnDailyBonusCells();
        InitCellsWithStaticData();
    }

    public void ParentCellAndResetScale(RectTransform parent)
    {
        DailyBonusCell cell = _dailyBonusCells[0];
        RectTransform cellRectTransform = cell.GetComponent<RectTransform>();

        for (int i = 0; i < _dailyBonusCells.Count; i++)
        {
            cell = _dailyBonusCells[i];
            cellRectTransform = cell.GetComponent<RectTransform>();
            cell.transform.SetParent(parent);
            cellRectTransform.localScale = Vector3.one;
        }
    }

    public void UpdateRewardCellsData(bool colldownPassed)
    {
        for (int i = 0; i < _dailyBonusCells.Count; i++)
        {
            _dailyBonusCells[i].SetRewardData(i, CurrentStreak, colldownPassed);
        }
    }

    public void SaveStreakData()
    {
        WriteStreakProgress();
        SaveStreakProgress();
    }

    private void WriteStreakProgress()
    {
        PlayerProgress progress = _progressService.Progress;
        progress.gameData.currentStreak = CurrentStreak;
        progress.gameData.maxStreak = MaxStreak;

        if (LastClaimTime.HasValue)
        {
            progress.gameData.lastClaimTime = LastClaimTime;
        }
    }

    private void SaveStreakProgress() =>
       _saveLoadService.SaveProgress();

    private void InitStreakValues(PlayerProgress progress)
    {
        try
        {
            LoadFromStaticData();

            if (progress != null)
            {
                LoadFromPlayerProgress(progress);
                TryLoadLastClaimTime(progress);

            }
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void LoadFromPlayerProgress(PlayerProgress progress)
    {
        MaxStreakReward = progress.gameData.maxStreakReward;
        CurrentStreak = progress.gameData.currentStreak;
        MaxStreak = progress.gameData.maxStreak;
        ClaimCooldown = progress.gameData.streakCooldown;
    }
    private void LoadFromStaticData()
    {
        MaxStreakReward = _dailyBonusStaticData.maxStreakReward;
        CurrentStreak = 0;
        MaxStreak = _dailyBonusStaticData.maxDaysStreak;
        ClaimCooldown = _dailyBonusStaticData.claimCooldown;
    }

    private void TryLoadLastClaimTime(PlayerProgress progress)
    {
        DateTime? time = progress.gameData.lastClaimTime;
        if (time.HasValue)
        {
            LastClaimTime = progress.gameData.lastClaimTime;
        }
    }

    private void SpawnDailyBonusCells()
    {
        try
        {
            _dailyBonusCells = new();

            for (int i = 0; i < MaxStreak; i++)
            {
                _dailyBonusCells.Add(_gameFactory.CreateDailyBonusCell().GetComponent<DailyBonusCell>());
            }
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    private void InitCellsWithStaticData()
    {
        try
        {
            DailyReward reward = _dailyBonusStaticData.rewards[0];

            for (int i = 0; i < _dailyBonusStaticData.rewards.Count; i++)
            {
                reward = _dailyBonusStaticData.rewards[i];
                _dailyBonusCells[i].InitCell(reward);
            }
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

}
