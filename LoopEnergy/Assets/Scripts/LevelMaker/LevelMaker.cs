#if UNITY_EDITOR
using System.Threading.Tasks;
using Puzzle.Match.Interface;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Puzzle.Match.TileGrid;
using Puzzle.Match.Tiles.Details;
using System;
using Unity.VisualScripting;


namespace Puzzle.Match.Controller
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
                data.Properties.RotationZ += 90;
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