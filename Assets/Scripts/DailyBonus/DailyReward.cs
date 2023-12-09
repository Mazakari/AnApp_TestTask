using UnityEngine;

public enum RewardType
{
    Tickets,
}

[System.Serializable]
public class DailyReward
{
    public Sprite sprite;
    public RewardType type;
    public int value;
    public int dayNumber;
}
