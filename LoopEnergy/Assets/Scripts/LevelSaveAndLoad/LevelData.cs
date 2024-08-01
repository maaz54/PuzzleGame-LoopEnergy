using System.Collections;
using System.Collections.Generic;
using EnergyLoop.Game.Interface;
using EnergyLoop.Game.Tiles.Details;
using UnityEngine;

namespace EnergyLoop.Game.LevelSerializer
{
    [System.Serializable]
    public class LevelData
    {
        public List<Level> Levels;

        public LevelData()
        {
            Levels = new List<Level>();
        }

    }

    [System.Serializable]
    public class Level
    {
        // public TileData[,] Grid;
        // public List<List<TileData>> Grid;
        public List<TileData> Grid;
        public int XLenght;
        public int YLenght;

        public Level(ITile[,] grid, int levelNo)
        {
            Grid = new();
            // this.Grid = new TileData[grid.GetLength(0), grid.GetLength(1)];
            // for (int x = 0; x < grid.GetLength(0); x++)
            // {
            //     List<TileData> tiles = new();
            //     for (int y = 0; y < grid.GetLength(1); y++)
            //     {
            //         // this.Grid[x, y] = grid[x, y].Data;
            //         tiles.Add(grid[x, y].Data);
            //     }
            //     Grid.Add(tiles);
            // }

            foreach (var tile in grid)
            {
                Grid.Add(tile.Data);
            }

            XLenght = grid.GetLength(0);
            YLenght = grid.GetLength(1);

            // Debug.Log("Grids: " + Grid.Count);
        }


    }
}