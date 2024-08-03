using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelsProgression
{
    public List<LevelProgressData> LevelsProgressData;

    public LevelsProgression(int TotalLevels)
    {
        LevelsProgressData = new();
        for (int i = 0; i < TotalLevels; i++)
        {
            LevelsProgressData.Add(new LevelProgressData(i + 1));
        }
    }
}

[System.Serializable]
public class LevelProgressData
{
    public int LevelNo;
    public int NoOfTurn;
    public bool IsLocked;
    public bool Completed;

    public LevelProgressData(int LevelNo)
    {
        this.LevelNo = LevelNo;
        NoOfTurn = 0;
        IsLocked = LevelNo == 1 ? false : true;
    }
}
