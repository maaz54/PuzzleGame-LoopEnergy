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
        [SerializeField] string fileName;
        [SerializeField] string folderPath;

        public LevelData Data = new LevelData();

        private void Start()
        {
            folderPath = Path.Combine(Application.dataPath, "Resources/" + fileName + ".json");

            Data = LoadLevelData();
            if (Data == null)
                Data = new LevelData();

        }

        public void SaveLevel(Level level)
        {
            Data.Levels.Add(level);

            string json = JsonUtility.ToJson(Data,true);

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