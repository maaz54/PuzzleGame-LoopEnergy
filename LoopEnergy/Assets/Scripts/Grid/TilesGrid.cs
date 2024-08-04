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
                    TileData data = new TileData(TileType.None, new(x, y));
                    tile.SetTileDetails(data);
                    tile.SetPosition(position);
                    gridTiles[x, y] = tile;
                    tileGridPositions[x, y] = position;
                    position.y += tilesOffset;
                    tile.Transform.name = "" + x + "," + y;
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
            foreach (var tile in gridTiles)
            {
                tile.SetMakeBGInvisible();
                if (tile.Data.Type != TileType.None &&
                    tile.Data.Type != TileType.Power)
                {
                    int rotateTimes = UnityEngine.Random.Range(0, 4);

                    for (int i = 0; i < rotateTimes; i++)
                    {
                        tile.RotateTile();
                    }
                }
            }

            CheckAllNodesMatched();
        }

        public bool CheckAllNodesMatched()
        {
            int rows = gridTiles.GetLength(0);
            int columns = gridTiles.GetLength(1);

            bool isAllNodesConnected = true;

            foreach (var tile in gridTiles)
            {
                if (tile.Data.Type != TileType.None &&
                tile.Data.Type != TileType.Power)
                {
                    PowerBreak(tile);

                    // Check Up connection
                    if (tile.Node.connections[0] == 1 &&
                    tile.Data.Properties.y < columns - 1)
                    {
                        Debug.Log(tile.Transform.name);

                        if (gridTiles[tile.Data.Properties.x, tile.Data.Properties.y + 1].Node.connections[2] == 1)
                        {
                            SupplyPower(tile, gridTiles[tile.Data.Properties.x, tile.Data.Properties.y + 1]);

                        }
                        else
                        {
                            isAllNodesConnected = false;
                        }
                    }
                    else if (tile.Node.connections[0] == 1)
                    {
                        isAllNodesConnected = false;
                    }

                    // Check Down connection
                    if (tile.Node.connections[2] == 1 &&
                    tile.Data.Properties.y > 0)
                    {
                        Debug.Log(tile.Transform.name);

                        if (gridTiles[tile.Data.Properties.x, tile.Data.Properties.y - 1].Node.connections[0] == 1)
                        {
                            SupplyPower(tile, gridTiles[tile.Data.Properties.x, tile.Data.Properties.y - 1]);

                        }
                        else
                        {
                            isAllNodesConnected = false;
                        }
                    }
                    else if (tile.Node.connections[2] == 1)
                    {
                        isAllNodesConnected = false;
                    }

                    // Check right connection
                    if (tile.Node.connections[1] == 1 &&
                    tile.Data.Properties.x < rows - 1)
                    {
                        if (gridTiles[tile.Data.Properties.x + 1, tile.Data.Properties.y].Node.connections[3] == 1)
                        {
                            // isAllNodesConnected = true;
                            SupplyPower(tile, gridTiles[tile.Data.Properties.x + 1, tile.Data.Properties.y]);

                        }
                        else
                        {
                            isAllNodesConnected = false;
                        }
                    }
                    else if (tile.Node.connections[1] == 1)
                    {
                        isAllNodesConnected = false;
                    }

                    // Check left connection
                    if (tile.Node.connections[3] == 1 &&
                    tile.Data.Properties.x > 0)
                    {
                        if (gridTiles[tile.Data.Properties.x - 1, tile.Data.Properties.y].Node.connections[1] == 1)
                        {
                            // isAllNodesConnected = true;
                            SupplyPower(tile, gridTiles[tile.Data.Properties.x - 1, tile.Data.Properties.y]);

                        }
                        else
                        {
                            isAllNodesConnected = false;
                        }
                    }
                    else if (tile.Node.connections[3] == 1)
                    {
                        isAllNodesConnected = false;
                    }

                }
            }

            return isAllNodesConnected;
        }


        void SupplyPower(ITile currentTile, ITile OtherTile)
        {
            if (OtherTile.IsConnectedWithPower)
            {
                currentTile.StartGlow();
            }
            else if (currentTile.IsConnectedWithPower)
            {
                OtherTile.StartGlow();
            }
            else
            {
                currentTile.StopGlowing();
                OtherTile.StopGlowing();
            }
        }

        void PowerBreak(ITile currentTile)
        {
            currentTile.StopGlowing();
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
    }
}