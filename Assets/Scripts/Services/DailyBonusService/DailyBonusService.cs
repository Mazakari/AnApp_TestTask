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

    public DailyBonusService(IGameFactory gameFactory) => 
        _gameFactory = gameFactory;

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

    private void InitStreakValues(PlayerProgress progress)
    {
        try
        {
            MaxStreakReward = _dailyBonusStaticData.maxStreakReward;
            CurrentStreak = 0;
            MaxStreak = _dailyBonusStaticData.maxDaysStreak;
            ClaimCooldown = _dailyBonusStaticData.claimCooldown;

            if (progress != null)
            {
                MaxStreakReward = progress.gameData.maxStreakReward;
                CurrentStreak = progress.gameData.currentStreak;
                MaxStreak = progress.gameData.maxStreak;
                ClaimCooldown = progress.gameData.streakCooldown;
            }
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
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
