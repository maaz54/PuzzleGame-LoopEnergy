using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnergyLoop.Game.Gameplay.Manager.UI;
using EnergyLoop.Game.Interface;
using EnergyLoop.Game.LevelSerializer;
using EnergyLoop.Game.TileGrid;
using UnityEngine;

namespace EnergyLoop.Game.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] LevelPrograssionSaveLoadUtility levelPrograssionSaveLoadUtility;
        [SerializeField] LevelSaveLoadUtility levelSaveLoadUtility;
        [SerializeField] TilesGrid tileGrid;
        [SerializeField] UIManager uIManager;
        LevelData levelData;
        LevelsProgression levelsProgression;
        private ITile[,] gridTiles;

        private int noOfTurns = 0;
        private int currentLevel = 0;

        private void Start()
        {
            LoadingUtilities();
            uIManager.SetLevelDetails(levelsProgression);
            uIManager.OnLevelButton += OnLevelButton;
            uIManager.OnGameStartButton += OnPlayGame;
        }

        void LoadingUtilities()
        {
            levelData = levelSaveLoadUtility.LoadLevelData();
            if (!levelPrograssionSaveLoadUtility.IsProgressFileSaved)
            {
                levelPrograssionSaveLoadUtility.InitializeLevelProgression(levelData);
                levelsProgression = levelPrograssionSaveLoadUtility.levelProgression;
            }
            else
            {
                levelsProgression = levelPrograssionSaveLoadUtility.LoadProgress();
            }
        }

        private void OnLevelButton(LevelProgressData levelProgressData)
        {
            if (!levelProgressData.IsLocked)
            {
                currentLevel = levelProgressData.LevelNo;
                uIManager.PlayGame();
                GenerateLevel(levelProgressData.LevelNo);
            }
        }

        private void OnPlayGame()
        {
            LevelProgressData levelProgressData = levelsProgression.LevelsProgressData.FindLast(level => !level.IsLocked);
            currentLevel = levelProgressData.LevelNo;
            uIManager.PlayGame();
            GenerateLevel(currentLevel);
        }


        private void GenerateLevel(int levelNo)
        {
            tileGrid.EmptyGrid();
            gridTiles = tileGrid.GenerateTiles(levelData.Levels[levelNo - 1].XLenght, levelData.Levels[levelNo - 1].YLenght);
            tileGrid.SetLevelDetails(levelData.Levels[levelNo - 1]);
            tileGrid.RandomizeRotation();
            TilesListener();
            noOfTurns = 0;
            uIManager.SetScore(noOfTurns);
        }

        /// <summary>
        /// Adding tiles listner when use click on tiles
        /// </summary>
        private void TilesListener()
        {
            foreach (var tile in gridTiles)
            {
                if (tile.Data.Type != Tiles.Details.TileType.None &&
                    tile.Data.Type != Tiles.Details.TileType.Power)
                {
                    tile.OnTileClicked.RemoveAllListeners();
                    tile.OnTileClicked.AddListener(OnTileClicked);
                }
            }
        }

        /// <summary>
        /// calls when user click on a tile
        /// changing tile type 
        /// </summary>
        /// <param name="clickedTile"></param>
        private void OnTileClicked(ITile clickedTile)
        {
            clickedTile.RotateTile();
            noOfTurns++;
            uIManager.SetScore(noOfTurns);
            // if (clickedTile.CurrentRotationIndex == clickedTile.Data.Properties.RotationIndex)
            {
                CheckedIsAllTileMatched();
            }
        }

        private void CheckedIsAllTileMatched()
        {
            if (tileGrid.CheckAllNodesMatched())
            {
                int levelPrograssionIndex = levelsProgression.LevelsProgressData.FindIndex(level => level.LevelNo == currentLevel); ;
                levelsProgression.LevelsProgressData[levelPrograssionIndex].NoOfTurn = noOfTurns;
                levelsProgression.LevelsProgressData[levelPrograssionIndex].Completed = true;
                if (currentLevel < levelsProgression.LevelsProgressData.Count - 1)
                {
                    levelsProgression.LevelsProgressData[levelPrograssionIndex + 1].IsLocked = false;
                }
                levelPrograssionSaveLoadUtility.SaveProgress(levelsProgression);

                uIManager.LevelComplete(noOfTurns);
            }

        }
    }
}