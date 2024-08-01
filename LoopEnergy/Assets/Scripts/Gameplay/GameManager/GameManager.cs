using System.Collections;
using System.Collections.Generic;
using EnergyLoop.Game.Interface;
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

        private ITile[,] gridTiles;

        private void Start()
        {
            levelData = levelSaveLoadUtility.LoadLevelData();

            GenerateLevel(2);
        }

        private void GenerateLevel(int levelNo)
        {
            gridTiles = tileGrid.GenerateTiles(levelData.Levels[levelNo - 1].XLenght, levelData.Levels[levelNo - 1].YLenght);
            tileGrid.SetLevelDetails(levelData.Levels[levelNo - 1]);
            tileGrid.RandomizeRotation();
            TilesListener();
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
            clickedTile.SetZRotation(clickedTile.Transform.localEulerAngles.z + 90);
        }


    }
}