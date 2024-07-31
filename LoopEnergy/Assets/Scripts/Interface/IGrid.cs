using System.Collections;
using System.Threading.Tasks;
using EnergyLoop.Game.LevelSerializer;
using UnityEngine;

namespace EnergyLoop.Game.Interface
{
    public interface IGrid
    {
        /// <summary>
        /// Generating tiles at runtime
        /// </summary>
        /// <param name="x">lenght of horizontal tiles</param>
        /// <param name="y">lenght of vertical tiles</param>
        ITile[,] GenerateTiles(int xLenght, int yLenght);

        void SetLevelDetails(Level level);

        /// <summary>
        /// remove tiles at runtime
        /// </summary>
        void EmptyGrid();

    }
}