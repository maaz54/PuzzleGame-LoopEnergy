using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Utility
{
    /// <summary>
    /// Represents the progression data for all levels.
    /// </summary>
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


    /// <summary>
    /// Represents the progression data for a single level.
    /// </summary>
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
}
