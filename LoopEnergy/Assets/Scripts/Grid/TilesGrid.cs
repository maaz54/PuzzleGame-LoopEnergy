using EnergyLoop.Game.Tiles;
using EnergyLoop.Game.Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using EnergyLoop.Game.Tiles.Details;
using EnergyLoop.Game.LevelSerializer;

namespace EnergyLoop.Game.TileGrid
{
    /// <summary>
    /// Use to handle grid and logic removing matching tiles
    /// </summary>
    public class TilesGrid : MonoBehaviour, IGrid
    {
        /// <summary>
        /// Tiles prefabes use to instantiate on runtime
        /// </summary>
        [SerializeField] private Tile tilePrefabe;
        /// <summary>
        /// distance between gride of tiles 
        /// </summary>
        [SerializeField] private float tilesOffset;
        /// <summary>
        /// Tiles gride
        /// </summary>
        private ITile[,] gridTiles;
        /// <summary>
        /// Holding tile positon of grid
        /// </summary>
        private Vector3[,] tileGridPositions;

        /// <summary>
        /// Generating tiles
        /// </summary>
        public ITile[,] GenerateTiles(int xLength, int yLength)
        {
            gridTiles = new ITile[xLength, yLength];
            tileGridPositions = new Vector3[xLength, yLength];
            Vector3 position = new(-xLength / 2, -yLength / 2, 0);
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ITile tile = Instantiate(tilePrefabe, transform);
                    TileData data = new TileData(TileType.None, new(x, y, 0));
                    tile.SetTileDetails(data);
                    tile.SetPosition(position);
                    gridTiles[x, y] = tile;
                    tileGridPositions[x, y] = position;
                    position.y += tilesOffset;
                }
                position.x += tilesOffset;
                position.y = -yLength / 2;
            }
            return gridTiles;
        }

        public void SetLevelDetails(Level level)
        {
            foreach (var tile in level.Grid)
            {
                gridTiles[tile.Properties.x, tile.Properties.y].SetTileDetails(tile);
            }
        }

        public void RandomizeRotation()
        {
            System.Random random = new System.Random();
            int[] rotationValues = { 90, 180, 270, 360 };

            foreach (var tile in gridTiles)
            {
                if (tile.Data.Type != TileType.None &&
                    tile.Data.Type != TileType.Power)
                {
                    int randomValue = rotationValues[random.Next(rotationValues.Length)];
                    tile.SetZRotation(randomValue);
                }
            }
        }

        /// <summary>
        /// remove tiles at runtime
        /// </summary>
        public void EmptyGrid()
        {
            if (!gridTiles.IsUnityNull())
            {
                foreach (var item in gridTiles)
                {
                    item.DestroyTile();
                }
            }
        }

        /// <summary>
        /// return the tile position against the tile index x and y
        /// </summary>
        private Vector3 GetPositionFromIndex(int x, int y)
        {
            return tileGridPositions[x, y];
        }
    }
}