using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EnergyLoop.Game.Tiles.Details;

namespace EnergyLoop.Game.Interface
{
    public class TileClickedEvent : UnityEvent<ITile> { }
    public interface ITile
    {
        // handle the callback when use click on tile
        TileClickedEvent OnTileClicked { get; set; }

        // tile transfrom
        Transform Transform { get; }
        // tile position
        Vector3 Position { get; }

        TileData Data{ get; }

        //setting tile Details
        void SetTileDetails(TileData details);
        // setting tile position
        void SetPosition(Vector3 position);
        // destroying tile
        void DestroyTile();

        void SetType(TileType type);
    }
}