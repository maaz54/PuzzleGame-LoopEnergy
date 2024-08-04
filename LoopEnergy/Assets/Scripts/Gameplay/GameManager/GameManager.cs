using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergyLoop.Game.Gameplay.Manager.UI;
using EnergyLoop.Game.Gameplay.SFX;
using EnergyLoop.Game.Interface;
using EnergyLoop.Game.LevelSerializer;
using EnergyLoop.Game.TileGrid;
using EnergyLoop.Game.Tiles.Details;
using TMPro;
using UnityEngine;
using ObjectPool;

namespace EnergyLoop.Game.Gameplay.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] LevelPrograssionSaveLoadUtility levelPrograssionSaveLoadUtility;
        [SerializeField] LevelSaveLoadUtility levelSaveLoadUtility;
        [SerializeField] TilesGrid tileGrid;
        [SerializeField] UIManager uIManager;
        [SerializeField] CameraBehavior cameraBehavior;
        [SerializeField] SFXPlayer sFXPlayer;
        [SerializeField] ObjectPooler objectPooler;

        LevelData levelData;
        LevelsProgression levelsProgression;
        private ITile[,] tiles;

        private int noOfTurns = 0;
        private int currentLevel = 0;

        bool isGameCompleted = false;

        private void Start()
        {
            tileGrid.Initialize(objectPooler);
            LoadingUtilities();
            uIManager.SetLevelDetails(levelsProgression);
            uIManager.OnLevelButton += OnLevelButton;
            uIManager.OnGameStartButton += OnPlayGame;
            uIManager.TapToContinue += OnContinueButton;

            tileGrid.PlaySFX += PlaySFX;
        }

        private void PlaySFX(string name)
        {
            sFXPlayer.PlayAudioClip(name);
        }

        private void OnContinueButton()
        {
            tileGrid.EmptyGrid();
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
                uIManager.PlayGame(currentLevel);
                GenerateLevel(levelProgressData.LevelNo);
            }
        }

        private void OnPlayGame()
        {
            LevelProgressData levelProgressData = levelsProgression.LevelsProgressData.FindLast(level => !level.IsLocked);
            currentLevel = levelProgressData.LevelNo;
            uIManager.PlayGame(currentLevel);
            GenerateLevel(currentLevel);

        }


        private void GenerateLevel(int levelNo)
        {
            tileGrid.EmptyGrid();
            tiles = tileGrid.GenerateTiles(levelData.Levels[levelNo - 1].XLenght, levelData.Levels[levelNo - 1].YLenght);
            tileGrid.SetLevelDetails(levelData.Levels[levelNo - 1]);
            tileGrid.RandomizeRotation();
            TilesListener();
            noOfTurns = 0;
            uIManager.SetScore(noOfTurns);
            cameraBehavior.AdjustCameraSize(levelData.Levels[levelNo - 1].XLenght, levelData.Levels[levelNo - 1].YLenght);
            isGameCompleted = false;
            cameraBehavior.TriggerShake();
            PlaySFX("tick");

        }

        /// <summary>
        /// Adding tiles listner when use click on tiles
        /// </summary>
        private void TilesListener()
        {
            foreach (var tile in tiles)
            {
                if (tile.Data.Type != Tiles.Details.TileType.None)// &&
                                                                  // tile.Data.Type != Tiles.Details.TileType.Power)
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
            if (!isGameCompleted)
            {
                if (clickedTile.Data.Type == TileType.Power)
                {
                    PlaySFX("foul");
                    cameraBehavior.TriggerShake();
                }
                else
                {

                    clickedTile.RotateTile();
                    noOfTurns++;
                    uIManager.SetScore(noOfTurns);

                    PlaySFX("tick");
                    _ = CheckedIsAllTileMatched();
                }
            }
        }

        private async Task CheckedIsAllTileMatched()
        {
            if (tileGrid.CheckAllNodesMatched())
            {
                int levelPrograssionIndex = levelsProgression.LevelsProgressData.FindIndex(level => level.LevelNo == currentLevel); ;
                levelsProgression.LevelsProgressData[levelPrograssionIndex].NoOfTurn = noOfTurns;
                levelsProgression.LevelsProgressData[levelPrograssionIndex].Completed = true;
                if (currentLevel < levelsProgression.LevelsProgressData.Count)
                {
                    levelsProgression.LevelsProgressData[levelPrograssionIndex + 1].IsLocked = false;
                }
                levelPrograssionSaveLoadUtility.SaveProgress(levelsProgression);

                PlaySFX("win");
                isGameCompleted = true;
                cameraBehavior.TriggerShake();
                await tileGrid.GlowAllTilesEffect();
                await Task.Delay(TimeSpan.FromSeconds(2));
                uIManager.LevelComplete(noOfTurns);
                uIManager.UpdateLevelDetails(levelsProgression);
            }

        }
    }
}