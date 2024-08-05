using System.Collections;
using System.Collections.Generic;
using System.IO;
using EnergyLoop.Game.LevelSerializer;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Utility
{
    public class LevelPrograssionSaveLoadUtility : MonoBehaviour
    {
        // Stores the levels progression data
        public LevelsProgression levelProgression;

        // Checks if the progress file exists
        public bool IsProgressFileSaved => File.Exists(GetSaveFilePath());

        /// <summary>
        /// return the full path of the save file.
        /// </summary>
        private string GetSaveFilePath()
        {
            return Path.Combine(Application.persistentDataPath, "Datafile.json");
        }

        /// <summary>
        /// Initializes the level progression with the given level data.
        /// </summary>
        public void InitializeLevelProgression(LevelData Data)
        {
            levelProgression = new LevelsProgression(Data.Levels.Count);
            SaveProgress(levelProgression);
        }

        /// <summary>
        /// Saves the current level progression to a file.
        /// </summary>
        public void SaveProgress(LevelsProgression levelProgression)
        {
            this.levelProgression = levelProgression;
            string json = JsonUtility.ToJson(levelProgression, true);
            File.WriteAllText(GetSaveFilePath(), json);
        }

        /// <summary>
        /// Loads the level progression from the save file.
        /// </summary>
        public LevelsProgression LoadProgress()
        {
            if (IsProgressFileSaved)
            {
                string json = File.ReadAllText(GetSaveFilePath());
                levelProgression = JsonUtility.FromJson<LevelsProgression>(json);
            }

            return levelProgression;
        }

    }
}