using System.Collections;
using System.Collections.Generic;
using EnergyLoop.Game.LevelSerializer;
using EnergyLoop.Game.TileGrid;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] LevelSaveLoadUtility levelSaveLoadUtility;
        [SerializeField] TilesGrid tileGrid;
        LevelData levelData;

        private void Start()
        {
            levelData = levelSaveLoadUtility.LoadLevelData();
            GenerateLevel(1);
        }

        private void GenerateLevel(int levelNo)
        {
            tileGrid.GenerateTiles(levelData.Levels[levelNo - 1].XLenght, levelData.Levels[levelNo - 1].YLenght);
            tileGrid.SetLevelDetails(levelData.Levels[levelNo - 1]);
        }


    }
}