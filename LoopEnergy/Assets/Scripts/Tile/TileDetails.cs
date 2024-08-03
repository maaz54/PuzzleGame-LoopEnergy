using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnergyLoop.Game.Tiles.Details
{
    [System.Serializable]
    public class TileView
    {
        public Sprite sprite;
        public TileType Type;
    }

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