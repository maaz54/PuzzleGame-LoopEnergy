using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnergyLoop.Game.Tiles.Details
{
    // Represents visual and type details of a tile
    [System.Serializable]
    public class TileView
    {
        public Sprite sprite;
        public TileType Type;
    }

    //Contains data for each tile, including its type and transformation properties
    [System.Serializable]
    public class TileData
    {
        public TileType Type;
        public TileTranformProperties Properties;

        public TileData(TileType Type, TileTranformProperties Properties)
        {
            this.Type = Type;
            this.Properties = Properties;
        }
    }

    // Holds the transformation properties of a tile, including position and rotation
    [System.Serializable]
    public class TileTranformProperties
    {
        public int x, y;
        public int RotationIndex;

        public TileTranformProperties(int x, int y, int RotationIndex)
        {
            this.x = x;
            this.y = y;
            this.RotationIndex = RotationIndex;
        }
    }

    //representing different types of tiles
    public enum TileType
    {
        None,
        Power,
        Bulb,
        Wire1,
        Wire2,
        Wire3
    }
}