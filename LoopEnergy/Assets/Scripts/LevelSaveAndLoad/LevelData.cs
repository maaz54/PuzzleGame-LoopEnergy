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
        public List<TileData> Grid;
        public int XLenght;
        public int YLenght;

        public Level(ITile[,] grid, int levelNo)
        {
            Grid = new();
            foreach (var tile in grid)
            {
                Grid.Add(tile.Data);
            }
            XLenght = grid.GetLength(0);
            YLenght = grid.GetLength(1);
        }
    }
}