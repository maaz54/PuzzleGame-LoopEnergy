using System.Collections;
using System.Collections.Generic;
using System.IO;
using EnergyLoop.Game.Interface;
using EnergyLoop.Game.Tiles.Details;
using Unity.VisualScripting;
using UnityEngine;

namespace EnergyLoop.Game.LevelSerializer
{
    public class LevelSaveLoadUtility : MonoBehaviour
    {
        // Name of the file to save/load data
        [SerializeField] string fileName;

        // Path to the folder where the file is saved
        [SerializeField] string folderPath;

        // hold level data
        public LevelData Data = new LevelData();

        private void Start()
        {
            folderPath = Path.Combine(Application.dataPath, "Resources/" + fileName + ".json");

            Data = LoadLevelData();
            if (Data == null)
                Data = new LevelData();

        }

        //  save a level to a JSON file
        public void SaveLevel(Level level)
        {
            Data.Levels.Add(level);

            string json = JsonUtility.ToJson(Data, true);

            string path = folderPath;
            File.WriteAllText(path, json);
            Debug.Log("Level saved to: " + path);
        }

        // Method to load the level data from the Resources folder
        public LevelData LoadLevelData()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
            if (jsonFile != null)
            {
                string json = jsonFile.text;
                return JsonUtility.FromJson<LevelData>(json);
            }
            else
            {
                Debug.LogError("File not found in Resources");
                return null;
            }
        }
    }
}