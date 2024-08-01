#if UNITY_EDITOR
using System.Threading.Tasks;
using EnergyLoop.Game.Interface;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using EnergyLoop.Game.TileGrid;
using EnergyLoop.Game.Tiles.Details;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using EnergyLoop.Game.LevelSerializer;


namespace EnergyLoop.Game.LevelMaker
{
    /// <summary>
    /// Handles the tiles and grid calling 
    /// </summary>
    public class LevelMaker : MonoBehaviour
    {
        [SerializeField] private TilesGrid TilesGrid;
        //// 
        [SerializeField] private int gridSizeX;
        //Grid size y 
        [SerializeField] private int gridSizeY;
        [SerializeField] LevelSaveLoadUtility levelSaveLoadUtility;
        [SerializeField] Button buttonLevelSave;
        /// <summary>
        /// Tiles Grid
        /// use to handle tiles listners like when use select tiles
        /// </summary>
        private ITile[,] gridTiles;
        private IGrid iGrid;

        bool isRotateTile;

        private void Awake()
        {
            this.iGrid = TilesGrid;
            buttonLevelSave.onClick.AddListener(SaveLevel);
        }

        private void SaveLevel()
        {
            levelSaveLoadUtility.SaveLevel(new Level(gridTiles, levelSaveLoadUtility.Data.Levels.Count + 1));
        }

        private void OnEnable()
        {
            Init();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRotateTile = !isRotateTile;
            }
        }

        private void Init()
        {
            iGrid.EmptyGrid();
            gridTiles = iGrid.GenerateTiles(gridSizeX, gridSizeY);
            TilesListner();
        }

        /// <summary>
        /// Adding tiles listner when use click on tiles
        /// </summary>
        private void TilesListner()
        {
            foreach (var tile in gridTiles)
            {
                tile.OnTileClicked.RemoveAllListeners();
                tile.OnTileClicked.AddListener(OnTileClicked);
            }
        }

        /// <summary>
        /// calls when user click on a tile
        /// changing tile type 
        /// </summary>
        /// <param name="clickedTile"></param>
        private void OnTileClicked(ITile clickedTile)
        {
            TileData data = clickedTile.Data;

            if (isRotateTile)
            {
                clickedTile.RotateTile();
                data.Properties.RotationIndex = clickedTile.CurrentRotationIndex;
            }
            else
            {
                int tileType = (int)data.Type;
                if (tileType >= Enum.GetValues(typeof(TileType)).Length - 1)
                {
                    tileType = 0;
                }
                else
                {
                    tileType++;
                }
                data.Type = (TileType)tileType;
            }

            clickedTile.SetTileDetails(data);
        }
    }
}
#endif