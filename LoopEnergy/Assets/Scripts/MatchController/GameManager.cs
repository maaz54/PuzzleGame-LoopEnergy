using System.Threading.Tasks;
using Puzzle.Match.Interface;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Puzzle.Match.TileGrid;
using Unity.VisualScripting;


namespace Puzzle.Match.Controller
{
    /// <summary>
    /// Handles the tiles and grid calling 
    /// </summary>
    public class GameManager : MonoBehaviour
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
        bool canSwipeTile = true;

        private void Start()
        {
            this.iGrid = TilesGrid;
            Init();
        }


        private void Init()
        {
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
                tile.OnTileSelect.RemoveListener(OnTileClicked);
                tile.OnTileSelect.AddListener(OnTileClicked);
            }
        }

        /// <summary>
        /// calls when user click on a tile
        /// </summary>
        /// <param name="selectedTile"></param>
        private void OnTileClicked(ITile selectedTile)
        {
            
        }
    }
}