using System.Collections;
using System.Collections.Generic;
using System.IO;
using EnergyLoop.Game.LevelSerializer;
using UnityEngine;

public class LevelPrograssionSaveLoadUtility : MonoBehaviour
{
    public LevelsProgression levelProgression;
    public bool IsProgressFileSaved => File.Exists(GetSaveFilePath());

    //get the full path of the save file
    private string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, "Datafile.json");
    }

    public void InitializeLevelProgression(LevelData Data)
    {
        levelProgression = new LevelsProgression(Data.Levels.Count);
        SaveProgress(levelProgression);
    }

    public void SaveProgress(LevelsProgression levelProgression)
    {
        this.levelProgression = levelProgression;
        string json = JsonUtility.ToJson(levelProgression, true);
        File.WriteAllText(GetSaveFilePath(), json);
    }

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
