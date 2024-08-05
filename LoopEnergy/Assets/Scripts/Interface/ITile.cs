using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EnergyLoop.Game.Tiles.Details;
using EnergyLoop.Game.Tiles;
using ObjectPool.Interface;

namespace EnergyLoop.Game.Interface
{
    /// <summary>
    /// Event triggered when a tile is clicked.
    /// </summary>
    public class TileClickedEvent : UnityEvent<ITile> { }

    public interface ITile : IPoolableObject
    {
        // handle the callback when use click on tile
        TileClickedEvent OnTileClicked { get; set; }

        // tile transfrom
        new Transform Transform { get; }
        // tile position
        Vector3 Position { get; }

        TileData Data { get; }

        Node Node { get; }

        int CurrentRotationIndex { get; }

        bool IsConnectedWithPower { get; set; }

        /// <summary>
        /// Sets the tile details and optionally initializes the node.
        /// </summary>
        void SetTileDetails(TileData details, bool initializeNode);


        /// <summary>
        /// Sets the position of the tile.
        /// </summary>
        void SetPosition(Vector3 position);

        /// <summary>
        /// Makes the background of the tile invisible.
        /// </summary>
        void SetMakeBGInvisible();


        /// <summary>
        /// Starts the glow effect for the tile.
        /// </summary>
        void StartGlow();

        /// <summary>
        /// Stops the glow effect for the tile.
        /// </summary>
        void StopGlowing();

        /// <summary>
        /// Sets the type of the tile.
        /// </summary>
        void SetType(TileType type);

        /// <summary>
        /// Rotates the tile.
        /// </summary>
        void RotateTile();
    }
}